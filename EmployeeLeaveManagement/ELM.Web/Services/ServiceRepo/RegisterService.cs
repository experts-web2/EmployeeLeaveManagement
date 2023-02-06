
using DomainEntity.Models;
using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Identity;


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
        public async Task RegisterEmployee(UserRegistrationModel userRegistrationModel)
        {
            await _httpService.PostAsJsonAsync(controllerRoute+"/register", userRegistrationModel);
        }

        public async Task DeleteUserbyId(string id)
        {
            await _httpService.DeleteAsync($"{controllerRoute}/{id}");
        }

        public async Task<List<User>> GetAllUser()
        {
            List<User> respons = new();
            try
            {
                respons = await _httpService.GetFromJsonAsync<List<User>>(controllerRoute);

            }
            catch (Exception)
            {
                respons = null;
            }
            return respons;
        }

        public async Task<bool> SignIn(LogIn login)
        {
            try
            {
                var massage = await _httpService.PostAsJsonAsync(controllerRoute + "/SignIn", login);
                if (massage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }         
          
        }

        public async Task SignOut()
        {
            await _httpService.GetFromJsonAsync<User>($"{controllerRoute}/logout");
        }

        public async Task UpdateUser(User user)
        {
            await _httpService.PutAsJsonAsync(controllerRoute, user);
        }
       public async Task<List<IdentityRole>> GetAllRoles()
        {
         return  await _httpService.GetFromJsonAsync<List<IdentityRole>>($"{controllerRoute}/getallroles");
        }
    }
}
