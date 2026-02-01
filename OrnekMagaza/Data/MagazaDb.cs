using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrnekMagaza.Models;

namespace OrnekMagaza.Data
{
    public class MagazaDb: IdentityDbContext<AppUser, AppRole, string>
    {
        public MagazaDb(DbContextOptions<MagazaDb> options) : base(options)
        {
        }
       public DbSet<Categories> Categories { get; set; }
       public DbSet<Products> Products { get; set; }
       public DbSet<ProductImage> ProductImages { get; set; }
    }
}
