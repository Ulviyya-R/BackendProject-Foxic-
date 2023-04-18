namespace Foxic_Backend_Project_.Entities
{
	public class BasketItem:BaseEntity
	{ 
		public decimal UnitPrice { get; set; }
	    public int SaleQuantity { get; set; }
	    public int ProductSizeColorId { get; set; }
		public int BasketId { get; set; }
		public Basket Basket { get; set; }

		public ProductSizeColor ProductSizeColor { get; set; }
	}
}
