using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrnekMagaza.Data;

namespace OrnekMagaza.Controllers
{
    public class ProductsCategoryController : Controller
    {
        private readonly MagazaDb _context;
        public ProductsCategoryController(MagazaDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var productsCategories = _context.Products.Include(x => x.Category).ToList();
            return View(productsCategories);
        }
    }
}
