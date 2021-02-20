using CroudSeek.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CroudSeek.Identity.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUserCS> userManager)
        {
            var applicationUser = new ApplicationUserCS
            {
                FirstName = "John",
                LastName = "Smith",
                UserName = "johnsmith",
                Email = "john@test.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "Plural&01?");
            }
        }
    }
}