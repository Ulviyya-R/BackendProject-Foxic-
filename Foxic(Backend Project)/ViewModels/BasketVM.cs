namespace Foxic_Backend_Project_.ViewModels
{
	public class BasketVM
	{
        public List<BasketItemVM> BasketItemsVM { get; set; }
        public decimal TotalPrice { get; set; }

        public BasketVM()
        {
            BasketItemsVM = new();
        }
    }
}
