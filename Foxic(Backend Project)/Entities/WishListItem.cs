using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Foxic_Backend_Project_.Entities
{
	public class WishListItem:BaseEntity
	{
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public User user { get; set; }
        [NotMapped]
        public int WishListId { get; set; }
        public WishList? WishList { get; set; }

        public int ProductSizeColorId { get; set; }
        public ProductSizeColor ProductSizeColor { get; set; }
    }
}
