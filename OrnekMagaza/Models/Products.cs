using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekMagaza.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ? CategoryID { get; set; }
        public Categories ? Category { get; set; }
        public decimal ? UnitPrice { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile ? ImageFile { get; set; }
    }
}
