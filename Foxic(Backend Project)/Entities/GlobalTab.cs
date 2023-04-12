namespace Foxic_Backend_Project_.Entities
{
	public class GlobalTab:BaseEntity
	{
		public string Text { get; set; }
		public List<Product> Products { get; set; }
	}
}
