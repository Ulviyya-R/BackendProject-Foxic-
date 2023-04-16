using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Controllers
{
	public class WishListController:Controller
	{
		private readonly FoxicDbContext _context;

		public WishListController(FoxicDbContext context)
		{
			_context = context;
		}
        public IActionResult Index(int id)
        {
            WishList? wishlist = _context.WishLists
                .Include(w => w.WishListItems)
                .SingleOrDefault(w => w.Id == id);

            if (wishlist == null)
            {
                return NotFound();
            }

            List<WishListItemVM> items = wishlist.WishListItems
                .Select(item => new WishListItemVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Desc,
                    ImageUrl = item.ImgUrl
                })
                .ToList();

            WishListVM viewModel = new WishListVM
            {
                Id = wishlist.Id,
                Name = wishlist.Name,
                WishListItemVMs = items
            };

            return View(viewModel);
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
                var wishlist = _context.WishLists.Find(wishlistItem.WishListId);
                wishlist.WishListItems.Add(wishlistItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(wishlistItem);
        }
        public async Task<IActionResult> AddItemToWishlist(int productId, int wishlistId)
        {
            // WishlistItem oluşturun ve verileri ayarlayın
            var product = await _context.Products.FindAsync(productId);
            var wishlistItem = new WishListItem
            {
                Name = product.Name,
                Price = product.Price,
                WishListId= wishlistId
            };

            // Wishlist'e öğe ekleyin
            var wishlist = _context.WishLists.Find(wishlistId);
            wishlist.WishListItems.Add(wishlistItem);

            // Veritabanına kaydedin
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = productId });
        }
    }
}
