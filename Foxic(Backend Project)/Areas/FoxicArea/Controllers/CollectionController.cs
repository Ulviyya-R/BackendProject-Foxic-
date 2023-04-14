using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic_Backend_Project_.Areas.FoxicArea.Controllers
{
	[Area("FoxicArea")]

	public class CollectionController:Controller
    {
		private readonly FoxicDbContext _context;

		public CollectionController(FoxicDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			IEnumerable<Collection> collections = _context.Collections.AsEnumerable();
			return View(collections);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Collection newCollection)
		{

			if (!ModelState.IsValid)
			{
				foreach (string message in ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage))
				{
					ModelState.AddModelError("", message);
				}
			}
			bool isdublicate = _context.Collections.Any(t => t.Name == newCollection.Name);
			if (isdublicate)
			{
				ModelState.AddModelError("", "You cannot enter the same data again");
				return View();
			}
			_context.Collections.Add(newCollection);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}


		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Collection? collection = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collection is null) return NotFound();
			return View(collection);
		}

		[HttpPost]
		public IActionResult Edit(int id, Collection editCollection)
		{
			if (id != editCollection.Id) return NotFound();
			Collection? collection = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collection is null) return NotFound();
			bool duplicate = _context.Collections.Any(c => c.Name == editCollection.Name && collection.Name != editCollection.Name);
			if (duplicate)
			{
				ModelState.AddModelError("Name", $"This '{collection.Name}' tag name is now available");
				return View();
			}
			collection.Name = editCollection.Name;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{
			if (id == 0) return NotFound();
			Collection? collection = _context.Collections.FirstOrDefault(c => c.Id == id);
			return collection is null ? BadRequest() : View(collection);
		}



		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Collection? collection = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collection is null) return NotFound();
			return View(collection);
		}

		[HttpPost]
		public IActionResult Delete(int id, Collection deleteCollection)
		{
			if (id != deleteCollection.Id) return NotFound();
			Collection? collection = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collection is null) return NotFound();
			_context.Remove(collection);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
