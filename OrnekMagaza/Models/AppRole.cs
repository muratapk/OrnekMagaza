using Microsoft.AspNetCore.Identity;

namespace OrnekMagaza.Models
{
    public class AppRole:IdentityRole<string>
    {
        public AppRole() : base()
        {
            Id = Guid.NewGuid().ToString();
        }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}
