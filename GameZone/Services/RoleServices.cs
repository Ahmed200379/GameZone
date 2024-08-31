using GameZone.Models;
using GameZone.viewmodel;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServices(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AddRole(RoleViewModel model)
        {
            IdentityRole modelvm = new IdentityRole();
            modelvm.Name = model.Name;
        IdentityResult result=    await _roleManager.CreateAsync(modelvm);
            if(result.Succeeded)
                {
                    return true;
                }
            else
            {
                return false;
            }
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
            await _userManager.AddToRoleAsync(user, "admin");
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
    }
}
