using Microsoft.AspNetCore.Identity;
using MPED.Application.Enums.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            //var allClaims = await roleManager.GetClaimsAsync(role);            
        }

        private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaim(adminRole, "Users");
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@MPED.pl",
                FirstName = "Admin",
                LastName = "MPED",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin1234");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
                await roleManager.SeedClaimsForAdmin();
            }
        }
    }
}
