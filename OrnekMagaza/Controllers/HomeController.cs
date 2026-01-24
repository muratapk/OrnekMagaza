using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;

namespace OrnekMagaza.Controllers
{
    public class HomeController : Controller
    {
        private readonly MagazaDb _context;
        public HomeController(MagazaDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Urundetay(int id)
        {
            //var sorgu = from urun in _context.Products
            //           where urun.ProductID == id
            //           select urun;
            //var urundetay = sorgu.FirstOrDefault();
            var liste = _context.Products.Where(x=>x.ProductID==id).FirstOrDefault();
            if (liste == null)
            {
                return NotFound("Ürün bulunamadı");
            }
            return View(liste);
        }
    }
}
