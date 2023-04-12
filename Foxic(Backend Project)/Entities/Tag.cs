namespace Foxic_Backend_Project_.Entities
{
	public class Tag:BaseEntity
	{
		public string Name { get; set; }
		public List<ProductTag> ProductTags { get; set; }
		public Tag()
		{
			ProductTags = new();
		}
	}
}
