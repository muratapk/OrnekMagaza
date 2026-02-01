using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Data;
using OrnekMagaza.Models;
using OrnekMagaza.ViewModel;

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
            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                AdSoyad = model.AdSoyad,
                Id = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
