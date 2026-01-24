using Microsoft.EntityFrameworkCore;
using OrnekMagaza.Models;

namespace MagazaApi.Data
{
    public class BaglanDb:DbContext
    {
        public BaglanDb(DbContextOptions<BaglanDb> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
