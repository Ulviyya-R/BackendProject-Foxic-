namespace Foxic_Backend_Project_.Entities
{
	public class Order:BaseEntity
	{
		public DateTime OrderDate { get; set; }
		public DateTime RequiredDate { get; set; }
		public DateTime DeliveredDate { get; set; }
		public string Fullname { get; set; }
		public decimal totalPrice { get; set; }
		public string UserId { get; set; }
		public int BasketId { get; set; }
		public Basket Basket { get; set; }

	}
}
