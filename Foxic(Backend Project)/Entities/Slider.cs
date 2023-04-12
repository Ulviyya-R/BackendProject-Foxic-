using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic_Backend_Project_.Entities
{
	public class Slider:BaseEntity
	{
		public string? ImagePath { get; set; }
		public string? Name { get; set; }
		public string? Text { get; set; }
		public string? ButtonText { get; set; }
		public byte? Order { get; set; }


		[NotMapped]
        public IFormFile? Image { get; set; }


	}
}
