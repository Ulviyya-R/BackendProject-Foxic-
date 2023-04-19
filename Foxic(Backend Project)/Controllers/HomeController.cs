using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Foxic_Backend_Project_.Controllers
{
    public class HomeController : Controller
    {
        private readonly FoxicDbContext _context;

        public HomeController(FoxicDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Slider> slider = _context.Sliders.OrderBy(s => s.Order).ToList();
			ViewBag.Products = _context.Products.Include(p=>p.ProductSizeColors).ThenInclude(psc=>psc.Color)
                                                 .Include(p=>p.ProductImages)
                                                 .Include(p=>p.Collection)
                                                 .Include(p=>p.ProductCategories).ThenInclude(pc=>pc.Category)
                                                 .Take(8).
                                                 ToList();
			return View(slider);
        }

		
		public IActionResult Search(string search)
        {
            var query = _context.Products.Include(p => p.ProductImages)
                                          .AsQueryable()
                                          .Where(p => p.Name.Contains(search));
            List<Product> products = query.OrderByDescending(p => p.Id).ToList();

            return PartialView("_SearchPartial",products);
                                       
        }

     
    }
}