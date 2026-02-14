using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;
using OrnekMagaza.Models;

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
            var liste = _context.Products.Where(x => x.ProductID == id).FirstOrDefault();
            if (liste == null)
            {
                return NotFound("Ürün bulunamadı");
            }
            return View(liste);
        }
        public IActionResult AddToCart(int id)
        {
            var urun = _context.Products.FirstOrDefault(x => x.ProductID == id);
            if (urun == null)
            {
                return NotFound("Ürün bulunamadı");
            }
            List<Products> Cart;
            var sessionCart = HttpContext.Session.GetString("Cart");
            if (sessionCart != null)
            {
                Cart = System.Text.Json.JsonSerializer.Deserialize<List<Products>>(sessionCart);
                //sepetin içinde ürün var ise json Serializer tipine çevir
            }
            else
            {
                Cart = new List<Products>();
                //sepet boş ise yeni bir liste oluştur
            }
            Cart.Add(urun);
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(Cart));

            return Json("Ürün sepete eklendi");
        }

        public IActionResult AddToList()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            if (sessionCart != null)
            {
                var Cart = System.Text.Json.JsonSerializer.Deserialize<List<Products>>(sessionCart);
                return View(Cart);
            }
            else
            {
                return View(new List<Products>());
            }
            return View();
        }
        public IActionResult RemoveFromCart(int id)
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            //sepettin tüm içeriğini al
            if (sessionCart != null)
            {
                //içerik boş degilse 
                var Cart = System.Text.Json.JsonSerializer.Deserialize<List<Products>>(sessionCart);
                //session get ile verileri json formatında al ve liste tipine çevir
                var urunToRemove = Cart.FirstOrDefault(x => x.ProductID == id);
                //bu listeden id'si eşit olan ürünü bul
                if (urunToRemove != null)
                {
                    //ürün bulunursa sepetten çıkar
                    Cart.Remove(urunToRemove);

                    HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(Cart));
                    //session 'a güncellenmiş sepeti json formatında kaydet
                    return RedirectToAction("AddToList","Home");
                }
            }
            return NotFound("Ürün bulunamadı");
        }

    }
}