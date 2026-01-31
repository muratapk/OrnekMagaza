using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekMagaza.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
        public int ? ProductId { get; set; }
        public Products ? Products { get; set; }
        public bool ? IsMain { get; set; }
        public string Title { get; set; }=string.Empty;

        [NotMapped]
        public IFormFile ? ImageFile { get; set; }= null;
    }
}
