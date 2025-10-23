using bookSystem.Models;
using bookSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace bookSystem.Controllers
{
    public class AccountController : Controller



    {

        private UserManager<ApplicationUser> userManager;

        private SignInManager<ApplicationUser> SignInManager;



        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {

            this.userManager = _userManager;
            this.SignInManager = _signInManager;


        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterService registerService)
        {
            if (ModelState.IsValid)
            {
                // Mapping
                var user = new ApplicationUser
                {
                    Address = registerService.Address,
                    UserName = registerService.User_name
                };

                // Create user securely (this hashes the password)
                IdentityResult result = await userManager.CreateAsync(user, registerService.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Category");
                }

                // Add any errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If ModelState is invalid or creation failed, redisplay the form
            return View("Register", registerService);
        }

    }
}