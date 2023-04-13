﻿using Foxic_Backend_Project_.DAL;
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
                                                 .Take(8).
                                                 ToList();
			return View(slider);
        }

     
    }
}