using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;
using OrnekMagaza.Models;
using OrnekMagaza.ViewModel;

namespace OrnekMagaza.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        //ekleme silme güncelleme işlemleri için
        private readonly SignInManager<AppUser> _signInManager;
        //login işlemleri için
        private readonly MagazaDb _context;
        //db işlemleri için
        public LoginController( UserManager<AppUser> userManager,  SignInManager<AppUser> signInManager,  MagazaDb context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            //bağımlılıkları constructor ile alıyoruz
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz giriş denemesi.");
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
