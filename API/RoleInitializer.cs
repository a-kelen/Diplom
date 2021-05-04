using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            Console.WriteLine(adminEmail);
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role { Name = "admin" });
                Console.WriteLine("sa");
            }

            if (await userManager.FindByEmailAsync(adminEmail) != null)
            {
                User admin = await userManager.FindByEmailAsync("admin@gmail.com");
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
