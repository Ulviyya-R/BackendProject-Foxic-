
namespace Foxic_Backend_Project_.Entities
{
	public class WishListItem:BaseEntity
	{
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int WishListQuantity { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

    }
}
