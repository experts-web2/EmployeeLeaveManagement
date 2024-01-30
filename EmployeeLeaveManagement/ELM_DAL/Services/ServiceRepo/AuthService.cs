using Blazored.LocalStorage;
using ELM.Shared;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace ELM_DAL.Services.ServiceRepo
{
    public class AuthService : ServiceBase, IAuthService
    {
        private IHttpClientService _httpservice;
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthService(IHttpClientService httpservice, IConfiguration configuration,
                         
                           ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor,AuthenticationStateProvider authenticationStateProvider,IJSRuntime jSRuntime,HttpClient httpService) : base(httpService, configuration, jSRuntime, authenticationStateProvider)
        {
            _httpservice = httpservice;
            _localStorage = localStorage;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResult> Register(UserRegistrationModel Userregister)
        {
             var result = await _httpservice.Post(Userregister, $"{Apiroute()}Auth/register/");
            return await _httpservice.DeserializeAsync<RegisterResult>(result);

        }

        public async Task<LoginResult> Login(LogIn Userlogin)
        {
            LoginResult result = new();
            try
            {
               var response = await _httpservice.Post(Userlogin, $"{Apiroute()}Auth/login/");
                
                if (!response.IsSuccessStatusCode) return new();

                result.Token = await response.Content.ReadAsStringAsync();
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(-1);
                httpContextAccessor?.HttpContext?.Request.Cookies.Append(new KeyValuePair<string, string>("jwt", result.Token ) );
                await _localStorage.SetItemAsync("jwt", result.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
                result.Successful = true;
                return result;
            }
            catch (Exception)
            {

            }

            return result;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwt");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}

