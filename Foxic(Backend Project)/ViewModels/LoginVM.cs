using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Foxic_Backend_Project_.ViewModels
{
	public class LoginVM
	{
		public string Username { get; set; }

		[DataType(DataType.Password)]

		public string Password { get; set; }


		public bool RememberMe { get; set; }

	}
}
