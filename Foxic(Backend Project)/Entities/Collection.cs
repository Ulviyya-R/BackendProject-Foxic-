namespace Foxic_Backend_Project_.Entities
{
	public class Collection:BaseEntity
	{
		public string Name { get; set; }
		public List<Product> products { get; set; }

		public Collection()
		{
			products = new();
		}
	}
}
