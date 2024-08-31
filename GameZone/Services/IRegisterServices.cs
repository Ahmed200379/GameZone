using GameZone.viewmodel;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Services
{
    public interface IRegisterServices
    {
        Task<List<IdentityError>> Register(RegisterUserViewModel model);
        void Logout();
        Task<bool> Login(LoginUserViewModel model);
    }
}
