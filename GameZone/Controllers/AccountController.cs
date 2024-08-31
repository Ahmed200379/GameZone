using GameZone.Services;
using GameZone.viewmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameZone.Controllers
{
    public class AccountController : Controller
    {
        public readonly IRegisterServices registeredServices;

        public AccountController(IRegisterServices registeredServices)
        {
            this.registeredServices = registeredServices;
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
                var error = await registeredServices.Register(model);
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
        public IActionResult Logout()
        {
            registeredServices.Logout();
            return RedirectToAction("Register");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if(ModelState.IsValid)
            {
             bool error= await registeredServices.Login(model);
                if(error)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User name or password are wrong");
                   
                }
            }
            return View(model);
        }
    }
}
