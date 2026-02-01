using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;
using OrnekMagaza.Models;

namespace OrnekMagaza.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        //ekleme silme güncelleme işlemleri için
        private readonly SignInManager<AppUser> _signInManager;
        //login işlemleri için
        private readonly MagazaDb _context;
        //db işlemleri için
        public AccountController( UserManager<AppUser> userManager,  SignInManager<AppUser> signInManager,  MagazaDb context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            //bağımlılıkları constructor ile alıyoruz
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
