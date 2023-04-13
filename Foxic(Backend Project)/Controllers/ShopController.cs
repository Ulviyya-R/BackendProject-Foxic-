using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Controllers
{
	public class ShopController:Controller
	{
		private readonly FoxicDbContext _context;

		public ShopController(FoxicDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			ViewBag.Products = _context.Products.Include(p => p.ProductSizeColors).ThenInclude(psc => psc.Color)
											 .Include(p => p.ProductImages)
											 .Include(p => p.Collection)
											 .Take(8).
											 ToList();
			return View();
		}

		public IActionResult Detail(int id)
		{
			if (id == 0) return NotFound();
			IQueryable<Product> products = _context.Products.AsNoTracking().AsQueryable();
			Product? product = products.Include(p=>p.ProductSizeColors).ThenInclude(psc=>psc.Color)
				                        .Include(p=>p.GlobalTabs)
				                         .Include(p => p.ProductImages)
				                       .AsSingleQuery().FirstOrDefault(p => p.Id == id);
			if (product == null) return NotFound();
			return View(product);
		}
	}
}
