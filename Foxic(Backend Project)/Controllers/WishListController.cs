using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic_Backend_Project_.Controllers
{
	public class WishListController:Controller
	{
		private readonly FoxicDbContext _context;

		public WishListController(FoxicDbContext context)
		{
			_context = context;
		}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WishListItem wishlistItem)
        {
            if (ModelState.IsValid)
            {
                // WishlistItem nesnesini veritabanına ekleyin
                _context.wishListItems.Add(wishlistItem);

                // Wishlist'e yeni bir öğe ekle
                var wishlist = _context.Wishlists.Find(wishlistItem.WishlistId);
                wishlist.Items.Add(wishlistItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(wishlistItem);
        }
    }
}
