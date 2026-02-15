using Microsoft.AspNetCore.Identity;

namespace OrnekMagaza.Models
{
    public class AppUser:IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string AdSoyad { get; set; } 
    }
}
