namespace Foxic_Backend_Project_.Entities
{
	public class ProductSizeColor : BaseEntity
	{
	
		public int ColorId { get; set; }
		public int SizeId { get; set; }
		public int Quantity { get; set; }

		public Product Product { get; set; } = null!;
		public Color Color { get; set; } = null!;
		public Size Size { get; set; } = null!;
		public ICollection<BasketItem>? BasketItems { get; set; }
		public ProductSizeColor()
		{
			BasketItems = new List<BasketItem>();
		}



	}
}
