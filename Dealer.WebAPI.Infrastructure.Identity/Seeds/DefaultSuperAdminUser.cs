using Dealer.WebAPI.Core.Application.Enums;
using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Dealer.WebAPI.Infrastructure.Identity.Seeds {
    public static class DefaultSuperAdminUser {
        public static async Task SuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            ApplicationUser SuperAdmin = new ApplicationUser {
                UserName = "SuperAdminUser",
                Email = "SuperAdmin@email.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "+987654321",
            };

            if (userManager.Users.All(u => u.Id != SuperAdmin.Id)) {
                var user = await userManager.FindByEmailAsync(SuperAdmin.Email);
                if (user == null) {
                    await userManager.CreateAsync(SuperAdmin, "123Pa$$word!");
                    await userManager.AddToRoleAsync(SuperAdmin, Roles.Client.ToString());
                    await userManager.AddToRoleAsync(SuperAdmin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(SuperAdmin, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
