using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites.Roles;
using Foxic_Backend_Project_.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Foxic_Backend_Project_.Controllers
{
	public class AccountController:Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<User> userManager,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Register(RegisterVM registeraccount)
		{
			if (!ModelState.IsValid) return View();
			if (!registeraccount.Terms) return View();
			User user = new User
			{
				UserName = registeraccount.Username,
				Fullname = String.Concat(registeraccount.Firstname, "", registeraccount.Lastname),
				Email = registeraccount.Email
			};
			IdentityResult result = await _userManager.CreateAsync(user, registeraccount.Password);
			if(!result.Succeeded)
			{
				foreach(IdentityError message in result.Errors)
				{
					ModelState.AddModelError("", message.Description);
				}
				return View();
			}
			await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
			return RedirectToAction("Login", "Account");

		}



		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM login)
		{
			if (!ModelState.IsValid) return View();
			User user = await _userManager.FindByNameAsync(login.Username);
			if(user == null)
			{
				ModelState.AddModelError("", "Username or password is incorrect");
				return View();
			}
			SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
			if (result.Succeeded)
			{
				if (result.IsLockedOut)
				{
					ModelState.AddModelError("", "Due to overthing your account has been blocked for 5 minutes");
					return View();
				}
				ModelState.AddModelError("", "Username or password is incorrect");
				return View();
			}
			return RedirectToAction("Index", "Home");

		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		//public async Task CreateRoles()
		//{
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
		//}
	}
}
