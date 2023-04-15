﻿namespace Foxic_Backend_Project_.Entities
{
	public class WishList:BaseEntity
	{
		public User user { get; set; }
		public ICollection<WishListItem> WishListItems { get; set; } = null!;

		public WishList()
		{
			WishListItems = new List<WishListItem>();
        }
	}
}
