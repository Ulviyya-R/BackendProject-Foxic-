
using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites.Extensions;
using Foxic_Backend_Project_.Utilites.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Areas.FoxicArea.Controllers
{
	[Area("FoxicArea")]
	[Authorize(Roles = "Admin, Moderator")]


	public class SliderController:Controller
	{
		private readonly FoxicDbContext _context;
		private readonly IWebHostEnvironment _env;

		public SliderController(FoxicDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}


		public IActionResult Index()
		{
			IEnumerable<Slider> sliders = _context.Sliders.AsEnumerable();
			return View(sliders);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Slider newSlider)
		{
			if (newSlider.Image is null)
			{
				ModelState.AddModelError("Image", "Please Select Image");
				return View();
			}
			if (!newSlider.Image.IsValidFile("image/"))
			{
				ModelState.AddModelError("Image", "Please Select Image Tag");
				return View();
			}
			if (!newSlider.Image.IsValidLength(2))
			{
				ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
				return View();
			}
			if (!Imports(newSlider))
			{
				return View();
			}
			var maxOrder = await _context.Sliders.OrderByDescending(s => s.Order).Select(s => s.Order).FirstOrDefaultAsync();
			var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			newSlider.ImagePath = await newSlider.Image.CreateImage(imagefolderPath, "slider");
			newSlider.Order = (byte)(maxOrder + 1);
			_context.Sliders.Add(newSlider);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id <= 0)
			{
				return NotFound();
			}
			Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (slider is null)
			{
				return BadRequest();
			}
			return View(slider);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Slider edited)
		{
			if (id != edited.Id) return NotFound();
			Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (!ModelState.IsValid) return View(slider);
			_context.Entry<Slider>(slider).CurrentValues.SetValues(edited);

			if (edited.Image is not null)
			{
				string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
				string filepath = Path.Combine(imagefolderPath, "slider", slider.ImagePath);
				FileUpload.DeleteImage(filepath);
				slider.ImagePath = await edited.Image.CreateImage(imagefolderPath, "slider");
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{
			if (id == 0) return NotFound();
			Slider? slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			return slider is null ? BadRequest() : View(slider);
		}


		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Slider? slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (slider is null) return NotFound();
			return View(slider);
		}

		[HttpPost]
		public IActionResult Delete(int id, Slider deleteslider)
		{
			if (id != deleteslider.Id) return NotFound();
			Slider? slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (slider is null) return NotFound();
			string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images","skins","fashion");
			string filepath = Path.Combine(imagefolderPath, "slider", slider.ImagePath);
			FileUpload.DeleteImage(filepath);
			_context.Sliders.Remove(slider);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		



		bool Imports(Slider newSlider)
		{
			if (newSlider.Name is null)
			{
				ModelState.AddModelError("", "Note the PlantName!");
				return false;
			}
			if (newSlider.Text is null)
			{
				ModelState.AddModelError("", "Note the Desc!");
				return false;
			}
			if (newSlider.ButtonText is null)
			{
				ModelState.AddModelError("", "Note the ButtonText!");
				return false;
			}
			if (newSlider.Order is null)
			{
				ModelState.AddModelError("", "Note the Order!");
				return false;
			}
			if (newSlider.Image is null)
			{
				ModelState.AddModelError("", "Note the LeftIcon!");
				return false;
			}
			if (newSlider.Order is null)
			{
				ModelState.AddModelError("", "Note the RightIcon!");
				return false;
			}
			return true;
		}
	}
}
