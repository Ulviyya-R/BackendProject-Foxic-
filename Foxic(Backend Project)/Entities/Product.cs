using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic_Backend_Project_.Entities
{
	public class Product:BaseEntity
	{
		public string Name { get; set; }
		[Column(TypeName = "decimal(6,2)")]
		public decimal Price { get; set; }
		[Column(TypeName = "decimal(6,2)")]
		public decimal? DiscountPrice { get; set; }
		public string ShortDesc { get; set; }
		public string LongDesc { get; set; }
		public int CollectionId { get; set; }
		public Collection Collection { get; set; }
		public int GlobalTabId { get; set; }
		public GlobalTab GlobalTabs { get; set; }
		public List<ProductCategory> ProductCategories { get; set; }
		public List<ProductTag> ProductTags { get; set; }
		public List<ProductImage> ProductImages { get; set; }

		public Product()
		{
			ProductCategories = new();
			ProductTags = new();
			ProductImages = new();

		}
	}
}
