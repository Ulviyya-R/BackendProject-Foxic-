namespace Foxic_Backend_Project_.Entities
{
	public class BasketItem:BaseEntity
	{ 
		public decimal UnitPrice { get; set; }
	    public int SaleQuantity { get; set; }
	    public int ProductSizeColorId { get; set; }
		public ProductSizeColor ProductSizeColor { get; set; }
	}
}
