using GameZone.viewmodel;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Services
{
    public interface IRoleServices
    {
        Task<bool> AddRole(RoleViewModel model);
        Task<List<IdentityError>> Register(RegisterUserViewModel model);
    }
}
