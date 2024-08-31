using GameZone.Services;
using GameZone.viewmodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameZone.Controllers
{
    [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private readonly IRoleServices roleServices;

        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = await roleServices.AddRole(model);
                if (result) 
                {
                    return RedirectToAction("index","home");
                }
                else
                {
                    ModelState.AddModelError("", "there are something wrong");
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var error = await roleServices.Register(model);
                if (error.Count > 0)
                {
                    foreach (var e in error)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
