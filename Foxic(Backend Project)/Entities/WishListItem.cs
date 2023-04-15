namespace Foxic_Backend_Project_.Entities
{
	public class WishListItem:BaseEntity
	{
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public User user { get; set; }


        public int ProductSizeColorId { get; set; }
        public ProductSizeColor ProductSizeColor { get; set; }
    }
}
