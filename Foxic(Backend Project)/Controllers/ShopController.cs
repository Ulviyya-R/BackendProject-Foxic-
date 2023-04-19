using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Numerics;

namespace Foxic_Backend_Project_.Controllers
{
	public class ShopController:Controller
	{
		private readonly FoxicDbContext _context;
		private readonly UserManager<User> _userManager;

		public ShopController(FoxicDbContext context,UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IActionResult Index(int page)
		{
			ViewBag.Products = _context.Products.Include(p => p.ProductSizeColors).ThenInclude(psc => psc.Color)
											 .Include(p => p.ProductImages)
											 .Include(p => p.Collection)
											 .Take(20).
											 ToList();
			return View();
		}

		static List<Product> RelatedProducts(IQueryable<Product> queryable, Product product, int id)
		{
			List<Product> relateds = new();

			product.ProductCategories.ForEach(pc =>
			{
				List<Product> related = queryable.
						Include(p => p.ProductImages).
						Include(p => p.ProductCategories).ThenInclude(p => p.Category).
						Include(p => p.ProductTags).ThenInclude(p => p.Tag).
                        Include(p => p.Collection).
							AsEnumerable().
							Where(p => p.ProductCategories.Contains(pc, new ProductCategoryComparer())
							&& p.Id != id && !relateds.Contains(p, new ProductComparer()))
								
							.ToList();
				relateds.AddRange(related);

			});
			product.ProductTags.ForEach(pc =>
			{
				List<Product> related = queryable.
						Include(p => p.ProductImages).
						Include(p => p.ProductCategories).ThenInclude(p => p.Category).
						Include(p => p.ProductTags).ThenInclude(p => p.Tag).
						Include(p => p.Collection).
							AsEnumerable().
							Where(p => p.ProductTags.Contains(pc, new ProductTagComparer())
							&& p.Id != id && !relateds.Contains(p, new ProductComparer()))

							.ToList();
				relateds.AddRange(related);

			});
	
			return relateds;
		}

		public IActionResult Detail(int id)
		{
			AllViewBagsData();
			if (id == 0) return NotFound();
			IQueryable<Product> products = _context.Products.AsNoTracking().AsQueryable();
			Product? product = products.Include(p=>p.ProductSizeColors).ThenInclude(psc=>psc.Color)
				                       .Include(p => p.ProductSizeColors).ThenInclude(psc => psc.Size)
                                       .Include(p => p.ProductComments).ThenInclude(cu => cu.User)
                                       .Include(p=>p.Collection)
										.Include(p=>p.GlobalTabs)
				                         .Include(p => p.ProductImages)
										 .Include(p=>p.ProductCategories).ThenInclude(pc=>pc.Category)
										 .Include(p => p.ProductTags).ThenInclude(p => p.Tag)
                                       .AsSingleQuery().FirstOrDefault(p => p.Id == id);

			ViewBag.RelatedProduct = RelatedProducts(products, product, id);

			if (product == null) return NotFound();
			return View(product);
		}

        [HttpPost]

        public async Task<IActionResult> AddComment(Comment comment, int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
			}
			else
			{
                Product? product = await _context.Products.Include(p => p.ProductComments).FirstOrDefaultAsync(p => p.Id == id);
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                Comment newComment = new Comment()
                {
                    Text = comment.Text,
                    User = user,
                    CreationTime = DateTime.UtcNow,
                    Product = product

                };
                user.Comments.Add(newComment);
                product.ProductComments.Add(newComment);
                await _context.Comments.AddAsync(newComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Detail), new { id });
            }
        }
		[HttpPost]
		public async Task<IActionResult> DeleteComment(int commentId)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}

			Comment comment = await _context.Comments
				.Include(c => c.User)
				.FirstOrDefaultAsync(c => c.Id == commentId);

			if (comment == null)
			{
				return NotFound();
			}

			if (comment.User.UserName != User.Identity.Name)
			{
				return Forbid();
			}

			_context.Remove(comment);
			await _context.SaveChangesAsync();
			


			return RedirectToAction(nameof(Detail), new { id = comment.ProductId });
		}



		/// <summary>
		/// I hid the viewbags I sent in actions here
		/// </summary>
		private void AllViewBagsData()
		{
			ViewBag.GlobalTabs = _context.GlobalTabs.AsEnumerable();
			ViewBag.Collections = _context.Collections.AsEnumerable();
			ViewBag.Categories = _context.Categories.AsEnumerable();
			ViewBag.Tags = _context.Tags.AsEnumerable();
			ViewBag.Sizes = _context.Sizes.AsEnumerable();
			ViewBag.Colors = _context.Colors.AsEnumerable();
		}



	}
}
