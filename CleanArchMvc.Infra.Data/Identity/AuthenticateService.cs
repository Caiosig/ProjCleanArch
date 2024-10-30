using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
	public class AuthenticateService : IAuthenticate
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
        }


        public async Task<bool> Authenticate(string email, string password)
		{
			//Utilizando ferramentas do identity para verificação a autenticação do usuário no login
			var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

			return result.Succeeded;
		}

		public async Task<bool> RegisterUser(string email, string password)
		{
			//Utilizando as ferramentas do identity para fazer o registro de um novo usuário
			var applicationUser = new ApplicationUser
			{
				UserName = email,
				Email = email
			};

			var result = await _userManager.CreateAsync(applicationUser, password);

			if (result.Succeeded)
				await _signInManager.SignInAsync(applicationUser, isPersistent: false);

			return result.Succeeded;
		}

		public async Task Logout()
		{
			//Utilizando as ferramentas do identity para fazer o logout do usuário
			await _signInManager.SignOutAsync();
		}
	}
}
