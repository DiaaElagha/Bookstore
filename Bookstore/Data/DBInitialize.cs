using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Data.Data
{
    public class DBInitialize
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                         serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var _roleManager =
                         serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //-------------- Initialize System Admin -------------------
                try
                {
                    if (!context.Users.Any())
                    {
                        var UserNameAdmin = "admin";
                        var PasswordAdmin = "admin11";
                        var PhoneNumberAdmin = "10002000";
                        var user = new IdentityUser
                        {
                            UserName = UserNameAdmin,
                            Email = UserNameAdmin,
                            PhoneNumber = PhoneNumberAdmin,
                            EmailConfirmed = true
                        };

                        var result = await _userManager.CreateAsync(user, PasswordAdmin);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }


    }
}
