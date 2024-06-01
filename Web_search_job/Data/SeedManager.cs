using Microsoft.AspNetCore.Identity;
using System.Data;
using System;
using Web_search_job.DatabaseClasses;
using Microsoft.EntityFrameworkCore;
using Web_search_job.DatabaseClasses.UserFolder;
using Microsoft.Extensions.DependencyInjection;

namespace Web_search_job.Data
{
    public static class SeedManager
    {
        private const string Admin = "Admin";
        private const string User = "Searcher";
        private const string Employer = "Employer";

        public static async Task Seed(IServiceProvider services)
        {
            await SeedRoles(services);

            await SeedAdminUser(services);
        }

        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any(r => r.Name == Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Admin));
            }

            if (!roleManager.Roles.Any(r => r.Name == User))
            {
                await roleManager.CreateAsync(new IdentityRole(User));
            }

            if (!roleManager.Roles.Any(r => r.Name == Employer))
            {
                await roleManager.CreateAsync(new IdentityRole(Employer));
            }
        }

        private static async Task SeedAdminUser(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "AuthenticationAdmin");

            if (adminUser is null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "AuthenticationAdmin",
                    Email = "your@email.com",
                    Provider = "Password"
                };
                await userManager.CreateAsync(adminUser, "VerySecretPassword!1");
                await userManager.AddToRoleAsync(adminUser, Role.Admin);
            }
        }
    }
}
