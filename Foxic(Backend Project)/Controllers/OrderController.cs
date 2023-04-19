using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Foxic_Backend_Project_.Controllers
{
	[Authorize]
	public class OrderController:Controller
	{
		private readonly FoxicDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrderController(FoxicDbContext context,UserManager<User> userManager)
		{
			_context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View(new List<WishListItem>());
			}

			var userrId = _userManager.GetUserId(User);

			var wishListItems = _context.WishListItems
				.Include(wli => wli.Product)
				.ThenInclude(p => p.ProductImages)
				.Where(wli => wli.UserId== userrId)
				.ToList();

			if (wishListItems.Count == 0)
			{
				return View(new List<WishListItem>());
			}

			return View(wishListItems);
		}

        public async Task<IActionResult> AddBasket(int productId, Product productBasket)
        {
            ProductSizeColor? productsc = _context.ProductSizeColors
                .Include(p => p.Product)
                .ThenInclude(p=>p.ProductImages)
                .FirstOrDefault(p => p.Product.Id == productId && p.SizeId == productBasket.AddcartVM.sizeId && p.ColorId == productBasket.AddcartVM.colorId);

            if (productsc is null) return NotFound();

            User? user = null;

			if (User.Identity.IsAuthenticated)
			{
				user = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			else
			{

				return RedirectToAction("Login", "Account");
			}


                Basket currentBasket = _context.Baskets
                    .Include(b => b.User)
                    .Include(b => b.BasketItems)
                    .ThenInclude(i => i.ProductSizeColor)
                    .FirstOrDefault(b => b.User.Id == user.Id && !b.IsOrdered);

                if (currentBasket == null)
                {
                currentBasket = new Basket()
                    {
                        User = user,
                        BasketItems = new List<BasketItem>(),
                    };
                    _context.Baskets.Add(currentBasket);
                }

                BasketItem currentBasketItem = currentBasket.BasketItems.FirstOrDefault(i => i.ProductSizeColor == productsc);

                if (currentBasketItem != null)
                {
                currentBasketItem.SaleQuantity += productBasket.AddcartVM.Quantity;
                }
                else
                {
                currentBasketItem = new BasketItem
                    {
                        UnitPrice = (decimal)productsc.Product.Price,
                        SaleQuantity = productBasket.AddcartVM.Quantity,
                        Basket = currentBasket,
                        ProductSizeColor = productsc
                };
                currentBasket.BasketItems.Add(currentBasketItem);

                }
			//currentBasket.TotalPrice = 0;
			//foreach (var item in currentBasket.BasketItems)
			//{
			//	currentBasket.TotalPrice += item.SaleQuantity * item.UnitPrice;
			//}


			await _context.SaveChangesAsync();
            

            return RedirectToAction("Index", "Shop");
        }

        public async Task<IActionResult> DeleteBasketItem(int id)
        {
            User? user = new();

			if (User.Identity.IsAuthenticated)
			{
				user = await _userManager.FindByNameAsync(User.Identity.Name);

			}
			else
			{
				return RedirectToAction("Login", "Account");
			}


			BasketItem? itemproducts = _context.BasketItems.FirstOrDefault(i => i.Id == id);

                if (itemproducts != null)
                {
                    Basket? userActiveBasket = _context.Baskets
                        .Include(b => b.User)
                        .Include(b => b.BasketItems)
                        .ThenInclude(i => i.ProductSizeColor)
                        .FirstOrDefault(b => b.User.Id == user.Id && !b.IsOrdered);

                    if (userActiveBasket != null)
                    {
                        userActiveBasket.BasketItems.Remove(itemproducts);
					userActiveBasket.TotalPrice = userActiveBasket.BasketItems.Sum(p => p.SaleQuantity * p.UnitPrice);

					_context.Remove(itemproducts);
                    _context.SaveChanges();
                    }
                }
            

            return RedirectToAction("Index", "Shop");
        }
        
        public async Task<IActionResult> AddWishList(int productId)
        {
			Product product = await _context.Products.FindAsync(productId);

			if (product == null) return NotFound();
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			User user = await _userManager.FindByNameAsync(User.Identity.Name);

          
            WishListItem userWishlistItem = await _context.WishListItems
                 .FirstOrDefaultAsync(x => x.UserId == user.Id && x.ProductId == productId);

            if (userWishlistItem is null)
            {
                userWishlistItem = new WishListItem
                {
                    UserId = user.Id,
                    ProductId = productId
                };
                _context.WishListItems.Add(userWishlistItem);
            }
            await _context.SaveChangesAsync();



            return RedirectToAction("Index", "Order");

		}
		public async Task<IActionResult> DeleteWishListItem(int id)
		{
			User? user = null;

			if (User.Identity.IsAuthenticated)
			{
				user = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}


			WishListItem? itemproducts = _context.WishListItems.FirstOrDefault(i => i.Id == id);

			if (itemproducts != null)
			{
				WishListItem? wishListItem = _context.WishListItems
					.Include(b => b.User)
					.FirstOrDefault(b => b.User.Id == user.Id);

				if (wishListItem != null)
				{
					_context.WishListItems.Remove(wishListItem);

					await _context.SaveChangesAsync();
				}
			}


			return RedirectToAction("Index", "Order");
		}


	}
}
