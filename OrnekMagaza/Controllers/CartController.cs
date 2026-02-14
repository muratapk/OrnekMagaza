using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Models;

namespace OrnekMagaza.Controllers
{
    public class CartController : Controller
    {
        [Authorize]
        public IActionResult Index()
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
    }
}
