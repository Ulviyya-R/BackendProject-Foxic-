using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Controllers
{
	public class CartController : Controller
	{
		private readonly FoxicDbContext _context;
		private readonly UserManager<User> _userManager;

		public CartController(FoxicDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			User user = await _userManager.FindByNameAsync(User.Identity.Name);

			Basket currentBasket = _context.Baskets
				.Include(b => b.BasketItems)
				.ThenInclude(i => i.ProductSizeColor)
				.ThenInclude(psc => psc.Product)
				.ThenInclude(p => p.ProductImages)
				.FirstOrDefault(b => b.User.Id == user.Id && !b.IsOrdered);

			if (currentBasket == null)
			{
				currentBasket = new Basket()
				{
					User = user,
					BasketItems = new List<BasketItem>(),
				};
				_context.Baskets.Add(currentBasket);
				await _context.SaveChangesAsync();
			}

			return View(currentBasket);
		}

		public async Task<IActionResult> RemoveCart(int id)
		{
			User user = await _userManager.FindByNameAsync(User.Identity.Name);
			Basket currentBasket = await _context.Baskets.FindAsync(id);


			if (currentBasket != null)
			{
				_context.Baskets.Remove(currentBasket);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> ClearAllCart(int id)
		{
			Basket basket = await _context.Baskets
				.Include(b => b.BasketItems)
				.FirstOrDefaultAsync(b => b.Id == id && !b.IsOrdered);

			if (basket == null)
			{
				return NotFound();
			}

			_context.BasketItems.RemoveRange(basket.BasketItems);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}