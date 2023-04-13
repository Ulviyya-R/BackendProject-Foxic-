namespace Foxic_Backend_Project_.Entities
{
	public class Color:BaseEntity
	{
		public string ColorImagePath { get; set; } = null!;

		public List<ProductSizeColor> ProductSizeColors { get; set; }

		public Color()
		{
			ProductSizeColors = new();
		}
	}
}
