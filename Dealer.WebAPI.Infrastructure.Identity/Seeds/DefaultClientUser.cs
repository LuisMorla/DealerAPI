using Dealer.WebAPI.Core.Application.Enums;
using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Dealer.WebAPI.Infrastructure.Identity.Seeds {
    public static class DefaultClientUser {
        public static async Task ClientAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            ApplicationUser ClientUser = new ApplicationUser {
                UserName = "ClientUser",
                Email = "client@email.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "+123456789",
            };

            if (userManager.Users.All(u => u.Id != ClientUser.Id)) {
                var user = await userManager.FindByEmailAsync(ClientUser.Email);
                if (user == null) {
                    await userManager.CreateAsync(ClientUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(ClientUser, Roles.Client.ToString());
                }
            }
        }
    }
}
