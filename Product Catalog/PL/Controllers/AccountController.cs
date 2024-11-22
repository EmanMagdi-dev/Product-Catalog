using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.DTOs;

namespace Product_Catalog.PL.Controllers
{


    public class AccountController : Controller
    {
        private readonly UserVM user;
        private readonly SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;

        public AccountController(UserVM userVM,
            SignInManager<IdentityUser> signInManager,
             UserManager<IdentityUser> userManager
            )
        {
            user = userVM;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(user);
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserVM user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identity = new IdentityUser();
                identity.UserName = user.UserName;
                identity.Email = user.Email;
                identity.PasswordHash = user.Password;
                IdentityResult result = await _userManager.CreateAsync(identity, user.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", result.ToString());
                }
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserVM user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser != null)
            {

                var result = await _signInManager.PasswordSignInAsync(identityUser, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            return View(user);
        }

        public async Task<IActionResult> signOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}

   

