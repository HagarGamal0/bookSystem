using bookSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookSystem.Controllers
{
    [Authorize(Roles ="Developer")]
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


        public async Task<IActionResult> SaveRole(RoleService roleService)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleService.RoleName;
                 IdentityResult result =await  RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    ViewBag.success = true;
                    return View("Create");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(" ", item.Description);
                }
            }

            return View("Create" , roleService);
        }
    }
}
