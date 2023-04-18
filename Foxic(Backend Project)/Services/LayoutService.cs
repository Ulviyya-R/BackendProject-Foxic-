using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Services
{
	public class LayoutService
	{
		private readonly FoxicDbContext _context;
		private readonly IHttpContextAccessor _accessor;
		private readonly UserManager<User> _userManager;

		public LayoutService(FoxicDbContext context, IHttpContextAccessor accessor, UserManager<User> userManager)
		{
			_context = context;
			_accessor = accessor;
			_userManager = userManager;
		}
		public Dictionary<string, string> GetSettings()
		{
			Dictionary<string, string> settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

			return settings;
		}
		

		public List<BasketItem>? GetBasketItem()
		{
			List<BasketItem> basketItem = _context.BasketItems
				                                          .Include(p => p.ProductSizeColor.Product).ThenInclude(p => p.ProductImages)
				                                           .ToList();
			return basketItem;
		}
		public List<Product> GetProducts()
		{
			List<Product> product = _context.Products.Include(p => p.ProductImages)
				                                      .ToList();
			return product;
		}
		
		public List<BasketItemVM> GetBasketItemVM()
		{
			List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
			User user = null;

			if (_accessor.HttpContext.User.Identity.IsAuthenticated)
			{
				user = _userManager.Users
					                 .FirstOrDefault(x => x.UserName == _accessor.HttpContext.User.Identity.Name);
			}


			List<BasketItem> items = _context.BasketItems
				                                  .Include(bi => bi.ProductSizeColor.Product).ThenInclude(psc => psc.ProductImages)
												  .Where(b => b.Basket.User.Id == user.Id).ToList();
		
			basketItemVMs = items.Select(bi => new BasketItemVM
			{
				ProductId = bi.ProductSizeColor.Product.Id,
				Price = (decimal)bi.ProductSizeColor.Product.DiscountPrice,
				ProductSizeColorId = bi.ProductSizeColorId,
				Quantity = bi.SaleQuantity
			}).ToList();

			return basketItemVMs;

		}
	}
}
