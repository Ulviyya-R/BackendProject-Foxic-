namespace Foxic_Backend_Project_.Entities
{
	public class Category:BaseEntity
	{
		public string Name { get; set; }
		public List<ProductCategory> ProductCategories { get; set; }

		public Category()
		{
			ProductCategories = new();
		}
	}
}
