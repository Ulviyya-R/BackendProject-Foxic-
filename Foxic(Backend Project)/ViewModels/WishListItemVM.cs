using Foxic_Backend_Project_.Entities;

namespace Foxic_Backend_Project_.ViewModels
{
	public class WishListItemVM
	{
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
