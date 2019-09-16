using ArvitumNew.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, int adminWorkPlace)
        {
            string adminName = "Admin";
            string adminEmail = "admin@gmail.com";
            string password = "Admin12345_";
            int adminWorkPlaceId = adminWorkPlace;

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("Дилер") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Дилер"));
            }
            if (await roleManager.FindByNameAsync("Модельер") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Модельер"));
            }
            if (await roleManager.FindByNameAsync("Производство") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Производство"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminName, WorkPlaceId = adminWorkPlaceId };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
