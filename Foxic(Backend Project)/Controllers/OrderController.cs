using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Controllers
{
	[Authorize]
	public class OrderController:Controller
	{
		private readonly FoxicDbContext _context;

		public OrderController(FoxicDbContext context)
		{
			_context = context;
		}

		//[HttpPost]
//		public async Task<IActionResult> AddBasket(int productId,Product basketProduct)
//		{
//			if (User.Identity.IsAuthenticated)
//			{
//				ProductSizeColor? product = _context.ProductSizeColors.Include(psc=>psc.Product).FirstOrDefault(p=>p.Product.Id == productId && p.SizeId == basketProduct.)
				
				

//			}



		}
}
