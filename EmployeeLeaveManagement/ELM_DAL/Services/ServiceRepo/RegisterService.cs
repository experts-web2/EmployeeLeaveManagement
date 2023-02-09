
using DomainEntity.Models;
using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class RegisterService : IRegisterService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        public RegisterService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpService = httpClient;
        }
        public async Task AddUserCall(UserRegistrationModel userRegistrationModel)
        {
            await _httpService.PostAsJsonAsync($"{Apiroute()}account/register", userRegistrationModel);
        }
        public async Task DeleteUserCall(string id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}account/{id}");
        }
        public async Task<List<User>> GetAllUserCall()
        {
            List<User> respons = new();
            try
            {
                respons = await _httpService.GetFromJsonAsync<List<User>>($"{Apiroute()}account");
            }
            catch (System.Exception)
            {
                respons = null;
            }
            return respons;
        }
        public async Task<bool> SignInCall(LogIn login)
        {
            var massage = await _httpService.PostAsJsonAsync($"{Apiroute()}account/SignIn", login);
            if (massage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task SignOut()
        {
            await _httpService.GetFromJsonAsync<User>($"{Apiroute()}account/logout");
        }
        public async Task UpdateUserCall(User user)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}account", user);
        }
        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _httpService.GetFromJsonAsync<List<IdentityRole>>($"{Apiroute}account/getallroles");
        }
        private string Apiroute()
        {
            var ApiRoute = _configuration["Api:Apiroute"];
            return ApiRoute;
        }
    }
}
