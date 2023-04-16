using System.ComponentModel.DataAnnotations;

namespace Foxic_Backend_Project_.ViewModels
{
	public class UserVM
	{
        public string? Username { get; set; }

        public string? Fullname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
