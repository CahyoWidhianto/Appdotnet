using Latihan.Data;
using Latihan.Models;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private  readonly AplicationDbContext _aplicationDbContext;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,  AplicationDbContext aplicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _aplicationDbContext = aplicationDbContext;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if(user != null)
            {
                //user found, check password
                var passswordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passswordCheck)
                {
                    // passsword correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                // password is incorrect
                TempData["Error"] = "Wrong credential. please, try again";
                return View(loginViewModel);
            }
            // user not found
            TempData["Error"] = "Wrong credential. please, try again";
            return View(loginViewModel);
        }
    }
}
