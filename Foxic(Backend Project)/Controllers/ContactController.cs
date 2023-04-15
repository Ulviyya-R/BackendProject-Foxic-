using Foxic_Backend_Project_.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Foxic_Backend_Project_.Controllers
{
    public class ContactController:Controller
    {
        private readonly FoxicDbContext _context;

        public ContactController(FoxicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Dictionary<string, string> settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
            return View(settings);
        }


    }
}
