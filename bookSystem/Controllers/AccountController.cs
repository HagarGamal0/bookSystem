using bookSystem.Models;
using bookSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
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
        [ValidateAntiForgeryToken] //request.form['requests']

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
                   await userManager.AddToRoleAsync(user, "Developer");
                      //add to cookie
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

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //request.form['requests']
        public async Task<IActionResult> Login(LoginService loginService)
        {

            if (ModelState.IsValid)
            {
             ApplicationUser user = await  userManager.FindByNameAsync(loginService.User_name);
                if (user != null)
                {
                 bool userFound =  await userManager.CheckPasswordAsync(user, loginService.Password);
                    if (userFound= true)
                    {

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("userAddress", user.Address));

                        await SignInManager.SignInWithClaimsAsync(user, loginService.remember_me , claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "username or password are wrong");

            }

            return View();
        }





        public async Task<IActionResult> SignOut()
        {

           await SignInManager.SignOutAsync();
            return View("Register");
        }


    }
}