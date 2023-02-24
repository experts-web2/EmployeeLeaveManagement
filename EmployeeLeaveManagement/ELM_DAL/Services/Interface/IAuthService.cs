using ELM.Shared;

namespace ELM_DAL.Services.Interface
{
    public interface IAuthService
    {
       Task<LoginResult> Login(LogIn Userlogin);
        Task Logout();
        Task<RegisterResult> Register(UserRegistrationModel Userregister);

    }
}
