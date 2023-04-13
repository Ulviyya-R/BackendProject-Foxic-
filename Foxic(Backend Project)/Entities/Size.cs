namespace Foxic_Backend_Project_.Entities
{
	public class Size:BaseEntity
	{
		public string Name { get; set; }
		public List<ProductSizeColor> ProductSizeColors { get; set; }

		public Size()
		{
			ProductSizeColors = new();
		}
	}
}
