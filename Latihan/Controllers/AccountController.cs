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

        }
    }
}
