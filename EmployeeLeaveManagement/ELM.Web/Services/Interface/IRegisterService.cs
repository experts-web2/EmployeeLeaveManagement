using DomainEntity.Models;
using ELM.Shared;
using Microsoft.AspNetCore.Identity;

namespace EmpLeave.Web.Services.Interface
{
    public interface IRegisterService
    {
        Task RegisterEmployee(UserRegistrationModel userRegistrationModel);
        Task DeleteUserbyId(string id);
        Task<List<User>> GetAllUser();
        Task SignOut();
        Task UpdateUser(User user);
        Task<List<IdentityRole>> GetAllRoles();
        Task<bool> SignIn(LogIn login);
    }
}
