using Microsoft.AspNetCore.Identity;
using OrnekMagaza.Models;

namespace OrnekMagaza.Data
{
    public class IdentityDemoData
    {
        public static class SeedData
        {
            public static async Task Initialize(IServiceProvider serviceProvider)
            {
                // Identity veritabanı ile ilgili başlangıç verileri burada eklenebilir.
                var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
                //bana kullanıcı yöneticisini ver
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                //bana rol yöneticisini ver
                string[] roleNames = { "Admin", "User", "Editor" };
                //Roller yoksa oluştur
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new AppRole(roleName));
                    }
                }
                //Admin kullanıcısı oluştur
                var administor = await userManager.FindByEmailAsync("admin@admin.com");
                if (administor == null)
                {
                    var adminUser = new AppUser
                    {
                        UserName = "Admin",
                        Email = "admin@admin.com",
                        AdSoyad = "Sistem Y",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        PhoneNumber = "5051234567"
                    };
                    var createAdminUser = await userManager.CreateAsync(adminUser, "Admin");
                    if (createAdminUser.Succeeded)
                    {
                        //Admin kullanıcısına rollerini ata
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        await userManager.AddToRoleAsync(adminUser, "User");
                        await userManager.AddToRoleAsync(adminUser, "Editor");
                    }
                }
                
                //Admin kullanıcısına rollerini ata
            }
        }
    }
}
