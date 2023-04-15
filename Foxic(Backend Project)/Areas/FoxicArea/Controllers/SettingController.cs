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

	public class SettingController : Controller
    {

        private readonly FoxicDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(FoxicDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            IEnumerable<Setting> settings = _context.Settings.AsEnumerable();
            return View(settings);
        }
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Setting newSetting)
		{
			if (newSetting.Image is null)
			{
				ModelState.AddModelError("Image", "Please Select Image");
				return View();
			}
			if (!newSetting.Image.IsValidFile("image/"))
			{
				ModelState.AddModelError("Image", "Please Select Image Tag");
				return View();
			}
			if (!newSetting.Image.IsValidLength(2))
			{
				ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
				return View();
			}

			var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			newSetting.SettingImagePath = await newSetting.Image.CreateImage(imagefolderPath, "settingImage");
			_context.Settings.Add(newSetting);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id <= 0)
			{
				return NotFound();
			}
			Setting? setting = _context.Settings.FirstOrDefault(s => s.Id == id);
			if (setting is null)
			{
				return BadRequest();
			}
			return View(setting);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Setting editedSetting)
		{
			if (id != editedSetting.Id) return NotFound();
			Setting? setting = _context.Settings.FirstOrDefault(s =>s.Id == id);
			if (!ModelState.IsValid) return View(setting);
			_context.Entry<Setting>(setting).CurrentValues.SetValues(editedSetting);

			if (editedSetting.Image is not null)
			{
				string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
				string filepath = Path.Combine(imagefolderPath, "settingImage", setting.SettingImagePath);
				FileUpload.DeleteImage(filepath);
				setting.SettingImagePath = await editedSetting.Image.CreateImage(imagefolderPath, "settingImage");
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{
			if (id == 0) return NotFound();
			Setting? setting = _context.Settings.FirstOrDefault(s => s.Id == id);
			return setting is null ? BadRequest() : View(setting);
		}


		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Setting? setting = _context.Settings.FirstOrDefault(s => s.Id == id);
			if (setting is null) return NotFound();
			return View(setting);
		}

		[HttpPost]
		public IActionResult Delete(int id, Setting deleteSetting)
		{
			if (id != deleteSetting.Id) return NotFound();
			Setting? setting = _context.Settings.FirstOrDefault(s => s.Id == id);
			if (setting is null) return NotFound();
			string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			string filepath = Path.Combine(imagefolderPath, "settingImage", setting.SettingImagePath);
			FileUpload.DeleteImage(filepath);
			_context.Settings.Remove(setting);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
