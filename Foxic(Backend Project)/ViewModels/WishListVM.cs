namespace Foxic_Backend_Project_.ViewModels
{
	public class WishListVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WishListItemVM> WishListItemVMs { get; set; }
    }
}
