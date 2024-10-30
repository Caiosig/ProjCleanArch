using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
	public class SeedUserRoleInitial : ISeedUserRoleInitial
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public void SeedUsers()
		{
			//Irá verificar se já existe um usuário com o mesmo e-mail
			if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
			{
				ApplicationUser user = new ApplicationUser();

				user.UserName = "usuario@localhost";
				user.Email = "usuario@localhost";
				user.NormalizedUserName = "USUARIO@LOCALHOST";
				user.NormalizedEmail = "USUARIO@LOCALHOST";
				user.EmailConfirmed = true;
				user.LockoutEnabled = false;
				user.SecurityStamp = Guid.NewGuid().ToString();

				IdentityResult result = _userManager.CreateAsync(user, "Numsey#2021").Result;

				//Caso seja realizado com sucesso, será incluido como usuário normal
				if (result.Succeeded)
					_userManager.AddToRoleAsync(user, "User").Wait();
			}

			//Irá verificar se já existe um ADMIN com o mesmo e-mail
			if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
			{
				ApplicationUser user = new ApplicationUser();

				user.UserName = "admin@localhost";
				user.Email = "admin@localhost";
				user.NormalizedUserName = "ADMIN@LOCALHOST";
				user.NormalizedEmail = "ADMIN@LOCALHOST";
				user.EmailConfirmed = true;
				user.LockoutEnabled = false;
				user.SecurityStamp = Guid.NewGuid().ToString();

				IdentityResult result = _userManager.CreateAsync(user, "Numsey#2021").Result;

				//Caso seja realizado com sucesso, será incluido como admin do sistema
				if (result.Succeeded)
					_userManager.AddToRoleAsync(user, "Admin").Wait();
			}
		}

		public void SeedRoles()
		{
			//Criação de roles para usuários de menos privilégios
			if (!_roleManager.RoleExistsAsync("User").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "User";
				role.NormalizedName = "USER";
				IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
			}

			//Criação de roles para usuários de maior privilégios
			if (!_roleManager.RoleExistsAsync("Admin").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Admin";
				role.NormalizedName = "ADMIN";
				IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
			}
		}
	}
}
