using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites.Extensions;
using Foxic_Backend_Project_.Utilites.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foxic_Backend_Project_.Areas.FoxicArea.Controllers
{
	[Area("FoxicArea")]
	[Authorize(Roles = "Admin, Moderator")]

	public class ColorController:Controller
    {
		private readonly FoxicDbContext _context;
		private readonly IWebHostEnvironment _env;

		public ColorController(FoxicDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}


		public IActionResult Index()
		{
			IEnumerable<Color> colors = _context.Colors.AsEnumerable();
			return View(colors);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Color newColor)
		{
			if (newColor.Image is null)
			{
				ModelState.AddModelError("Image", "Please Select Image");
				return View();
			}
			if (!newColor.Image.IsValidFile("image/"))
			{
				ModelState.AddModelError("Image", "Please Select Image Tag");
				return View();
			}
			if (!newColor.Image.IsValidLength(2))
			{
				ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
				return View();
			}
		
			var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			newColor.ColorImagePath = await newColor.Image.CreateImage(imagefolderPath, "color");
			_context.Colors.Add(newColor);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id <= 0)
			{
				return NotFound();
			}
			Color? color = _context.Colors.FirstOrDefault(c => c.Id == id);
			if (color is null)
			{
				return BadRequest();
			}
			return View(color);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Color edited)
		{
			if (id != edited.Id) return NotFound();
			Color? color = _context.Colors.FirstOrDefault(c => c.Id == id);
			if (!ModelState.IsValid) return View(color);
			_context.Entry<Color>(color).CurrentValues.SetValues(edited);

			if (edited.Image is not null)
			{
				string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
				string filepath = Path.Combine(imagefolderPath, "color", color.ColorImagePath);
				FileUpload.DeleteImage(filepath);
				color.ColorImagePath= await edited.Image.CreateImage(imagefolderPath, "color");
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{
			if (id == 0) return NotFound();
			Color? color = _context.Colors.FirstOrDefault(c => c.Id == id);
			return color is null ? BadRequest() : View(color);
		}


		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Color? color = _context.Colors.FirstOrDefault(c => c.Id == id);
			if (color is null) return NotFound();
			return View(color);
		}

		[HttpPost]
		public IActionResult Delete(int id, Color deletecolor)
		{
			if (id != deletecolor.Id) return NotFound();
			Color? color = _context.Colors.FirstOrDefault(c => c.Id == id);
			if (color is null) return NotFound();
			string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			string filepath = Path.Combine(imagefolderPath, "color", color.ColorImagePath);
			FileUpload.DeleteImage(filepath);
			_context.Colors.Remove(color);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}





	}
}
