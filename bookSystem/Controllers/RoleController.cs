using bookSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookSystem.Controllers
{
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager ){
            RoleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        public IActionResult SaveRole(RoleService roleService)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View("Create" , roleService);
        }
    }
}
