using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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
			ViewBag.Products = _context.Products.Include(p => p.ProductSizeColors).ThenInclude(psc => psc.Color)
											 .Include(p => p.ProductImages)
											 .Include(p => p.Collection)
											 .Take(8).
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
			if (id == 0) return NotFound();
			IQueryable<Product> products = _context.Products.AsNoTracking().AsQueryable();
			Product? product = products.Include(p=>p.ProductSizeColors).ThenInclude(psc=>psc.Color)
				                       .Include(p => p.ProductSizeColors).ThenInclude(psc => psc.Size)
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

	}
}
