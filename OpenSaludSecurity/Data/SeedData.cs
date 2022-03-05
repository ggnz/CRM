using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenSaludSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSaludSecurity.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw = "")
        {
            using (var context = new ApplicationDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.RequestAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.RequestManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                              string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Request.Any())
            {
                return;   // DB has been seeded
            }

            context.Request.AddRange(
                new Request
                {
                    Name = "Debra Garcia",
                    Email = "debra@example.com",

                    Status = RequestStatus.Approved,
                    OwnerID = adminID
                },
                new Request
                {
                    Name = "Thorsten Weinrich",
                    Email = "thorsten@example.com"
                },
             new Request
             {
                 Name = "Yuhong Li",
                 Email = "yuhong@example.com",

                 Status = RequestStatus.Submitted,
                 OwnerID = adminID
             },
             new Request
             {
                 Name = "Jon Orton",
                 Email = "jon@example.com",

                 Status = RequestStatus.Rejected,
                 OwnerID = adminID
             },
             new Request
             {
                 Name = "Diliana Alexieva-Bosseva",
                 Email = "diliana@example.com",

                 Status = RequestStatus.Submitted,
                 OwnerID = adminID
             },
             new Request
             {
                 Name = "Diliana2 Alexieva-Bosseva",
                 Email = "diliana2@example.com",
                 
                 OwnerID = adminID
             }
             );
            context.SaveChanges();
        }

    }
}
