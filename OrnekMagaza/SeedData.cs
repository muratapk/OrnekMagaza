using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrnekMagaza.Models;

namespace OrnekMagaza
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string[] roleNames = { "Admin", "Musteri" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AppRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };

                    await roleManager.CreateAsync(role);
                }
            }

            await CreateAdminUserAsync(userManager);
            await CreateTestUserAsync(userManager);
        }

        private static async Task CreateAdminUserAsync(UserManager<AppUser> userManager)
        {
            var adminEmail = "admin@ticaret.net";
            var adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    AdSoyad = "Sistem Yönetici",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Admin kullanıcı oluşturulamadı");
                }

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        private static async Task CreateTestUserAsync(UserManager<AppUser> userManager)
        {
            var testEmail = "musteri@test.com";
            var testPassword = "Musteri123!";

            var testUser = await userManager.FindByEmailAsync(testEmail);

            if (testUser == null)
            {
                testUser = new AppUser
                {
                    UserName = testEmail,
                    Email = testEmail,
                    AdSoyad = "Test Musteri",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(testUser, testPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "Musteri");
                }
            }
        }
    }
}