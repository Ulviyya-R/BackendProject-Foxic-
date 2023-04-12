namespace Foxic_Backend_Project_.Entities
{
	public class ProductCategory : BaseEntity
	{
		public int CategoryId { get; set; }
		public Product Product { get; set; }
		public Category Category { get; set; }
	}
}
