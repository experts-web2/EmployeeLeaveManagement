
using DomainEntity.Models;
using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class RegisterService : IRegisterService
    {
        private HttpClient _httpService;
        private string controllerRoute = "https://localhost:7150/api/account";
        public RegisterService(HttpClient httpClient)
        {
            _httpService = httpClient;
        }
        public async Task AddUserCall(UserRegistrationModel userRegistrationModel)
        {
            await _httpService.PostAsJsonAsync(controllerRoute+"/register", userRegistrationModel);
        }

        public async Task DeleteUserCall(string id)
        {
            await _httpService.DeleteAsync($"{controllerRoute}/{id}");
        }

        public async Task<List<User>> GetAllUserCall()
        {
            List<User> respons = new();
            try
            {
                respons = await _httpService.GetFromJsonAsync<List<User>>(controllerRoute);

            }
            catch (System.Exception)
            {
                respons = null;
            }
            return respons;
        }

        public async Task SignInCall(LogIn login)
        {
            await _httpService.PostAsJsonAsync(controllerRoute+ "/SignIn", login);
        }

        public async Task SignOut()
        {
            await _httpService.GetFromJsonAsync<User>($"{controllerRoute}/logout");
        }

        public async Task UpdateUserCall(User user)
        {
            await _httpService.PutAsJsonAsync(controllerRoute, user);
        }
       public async Task<List<IdentityRole>> GetAllRoles()
        {
         return  await _httpService.GetFromJsonAsync<List<IdentityRole>>($"{controllerRoute}/getallroles");
        }
    }
}
