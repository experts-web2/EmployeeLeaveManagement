using DomainEntity.Models;
using ELM.Shared;
using Microsoft.AspNetCore.Identity;

namespace EmpLeave.Web.Services.Interface
{
    public interface IRegisterService
    {
        Task AddUserCall(UserRegistrationModel userRegistrationModel);
        Task DeleteUser(string id);
        Task<List<User>> GetAllUserCall();
        Task SignOut();
        Task UpdateUserCall(User user);
        Task<List<IdentityRole>> GetAllRoles();
        Task<bool> SignInCall(LogIn login);
    }
}
