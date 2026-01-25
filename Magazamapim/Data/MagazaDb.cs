using Magazamapim.Models;
using Microsoft.EntityFrameworkCore;

namespace Magazamapim.Data
{
    public class MagazaDb:DbContext
    {
        public MagazaDb(DbContextOptions<MagazaDb> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
