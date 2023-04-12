using Foxic_Backend_Project_.DAL;
using Microsoft.AspNetCore.Mvc;

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
			return View();
		}
	}
}
