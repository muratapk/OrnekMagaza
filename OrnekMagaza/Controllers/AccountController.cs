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
        private readonly RoleManager<AppRole> _roleManager;
        //login işlemleri için
        private readonly MagazaDb _context;
        //db işlemleri için
        public AccountController( UserManager<AppUser> userManager,  SignInManager<AppUser> signInManager,  MagazaDb context,RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

                
                //kullanıcı rol tablosu Musteri olarak bunu ekle
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            if (!await _roleManager.RoleExistsAsync("Musteri"))
            {
                // await _roleManager.CreateAsync(new AppRole("Musteri"));
                var role = new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Musteri",
                    NormalizedName = "MUSTERI"
                };
                await _roleManager.CreateAsync(role);
            }
            //rol tanımlayı ata ve kullanıcıya rolü ekle
            await _userManager.AddToRoleAsync(user, "Musteri");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logoout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// /login sayfasına yönlendirme işlemi yapar. Eğer kullanıcı zaten giriş yapmışsa anasayfaya yönlendirir.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
