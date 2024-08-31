using GameZone.Models;
using GameZone.viewmodel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameZone.Services
{
    public class RegisterServices : IRegisterServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public async Task<List<IdentityError>> Register(RegisterUserViewModel model)
        {
            var errors = new List<IdentityError>();

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Country = model.Country,
                PasswordHash = model.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "user");
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors);
            }
            else
            {
                //create cookie
                await _signInManager.SignInAsync(user, false);
            }

            return errors;
        }
        public async Task<bool> Login(LoginUserViewModel model)
        {
            var errors = new List<IdentityError>();
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                bool foundpass = await _userManager.CheckPasswordAsync(user, model.Password);
                if (foundpass)
                {
                    await _signInManager.SignInAsync(user, model.rememberMe);
                    return true;

                }
            }
            return false;
        }
    }
}
