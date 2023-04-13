using Microsoft.Build.Framework;

namespace Foxic_Backend_Project_.ViewModels
{
	public class LoginVM
	{
		public string Username { get; set; }
		public string Password { get; set; }
		[Required]
		public bool RememberMe { get; set; }

	}
}
