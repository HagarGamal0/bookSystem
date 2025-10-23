using bookSystem.Models;
using bookSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookSystem.Controllers
{
    public class AccountController : Controller



    {

        private UserManager<ApplicationUser> userManager;


        public AccountController(UserManager<ApplicationUser> _userManager) { 
        
            this.userManager = _userManager;
       

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
        public async Task<IActionResult> Register( RegisterService registerService)
        {

            if (ModelState.IsValid) {
          //mapping 
                ApplicationUser user = new ApplicationUser();
                user.Address = registerService.Address;
                user.UserName = registerService.User_name;
                user.PasswordHash = registerService.Password;




              IdentityResult Result =  await userManager.CreateAsync(user);

                if (Result.Succeeded) {

            }
                foreach (var item in ViewData)
                {
                    
                }

                ApplicationUser applicationUser = new ApplicationUser();
            return View("Register" , registerService);
        }
    }
}
