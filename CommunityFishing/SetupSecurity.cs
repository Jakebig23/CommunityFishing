using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityFishing
{
	public static class SetupSecurity
	{
		public static void SeedUsers(UserManager<IdentityUser> userManager)
		{
			IdentityUser admin = userManager.FindByEmailAsync("admin@communityfishing.com").Result;
			if (admin == null)
			{
				IdentityUser sysAdmin = new IdentityUser();
				sysAdmin.Email = "admin@communityfishing.com";
				sysAdmin.UserName = "admin@communityfishing.com";

				IdentityResult result = userManager.CreateAsync(sysAdmin, "@Admin1").Result;
				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(sysAdmin, "Administrator").Wait();
				}
			}
		}

		public static void SeedRoles(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.RoleExistsAsync("NormalUser").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "NormalUser";
				IdentityResult roleResult = roleManager.CreateAsync(role).Result;
			}
			if (!roleManager.RoleExistsAsync("Administrator").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Administrator";
				IdentityResult roleResult = roleManager.CreateAsync(role).Result;
			}
		}
	}
}
