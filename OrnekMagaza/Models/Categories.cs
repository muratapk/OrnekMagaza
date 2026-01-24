using System.ComponentModel.DataAnnotations;

namespace OrnekMagaza.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public ICollection<Products>? Products { get; set; }
        //birden fazla ürün kategorisi olabilir


    }
}
