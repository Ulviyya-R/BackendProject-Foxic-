using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic_Backend_Project_.Entities
{
	public class Color:BaseEntity
	{
		public string ColorImagePath { get; set; } = null!;
		public string Name { get; set; }

		public List<ProductSizeColor> ProductSizeColors { get; set; }

		[NotMapped]
		public IFormFile? Image { get; set; }

		public Color()
		{
			ProductSizeColors = new();
		}
	}
}
