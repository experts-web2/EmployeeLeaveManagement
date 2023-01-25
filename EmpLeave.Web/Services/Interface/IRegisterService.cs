using DomainEntity.Models;
using ELM.Shared;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface IRegisterService
    {
        Task AddUserCall(UserRegistrationModel userRegistrationModel);
        Task DeleteUserCall(string id);
        Task<List<User>> GetAllUserCall();
        Task SignInCall(LogIn signIn);
        Task SignOut();
        Task UpdateUserCall(User user);
        Task<List<IdentityRole>> GetAllRoles();
    }
}
