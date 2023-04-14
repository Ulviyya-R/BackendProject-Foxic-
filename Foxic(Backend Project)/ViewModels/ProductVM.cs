using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Foxic_Backend_Project_.Entities;

namespace Foxic_Backend_Project_.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public int GlobalTabId { get; set; }
        public int CollectionId { get; set; } 
        [NotMapped]
        public ICollection<int> CategoryIds { get; set; } = null!;
        [NotMapped]
        public ICollection<int> TagIds { get; set; } = null!;
        [NotMapped]
        public IFormFile? MainPhoto { get; set; }
       
        [NotMapped]
        public ICollection<IFormFile>? Images { get; set; }
        public ICollection<ProductImage>? AllImages { get; set; }
        public ICollection<int>? ImagesId { get; set; }

        public string? ColorsSizesQuantity { get; set; }
        public string? DeleteColorSize { get; set; }
        public List<ProductSizeColor> ProductSizeColor { get; set; }

        public ProductVM()
        {
            ProductSizeColor = new();
        }
    }
}
