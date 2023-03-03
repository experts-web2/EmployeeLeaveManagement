using Blazored.LocalStorage;
using ELM.Shared;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace ELM_DAL.Services.ServiceRepo
{
    public class AuthService : IAuthService
    {
        private IHttpClientService _httpService;
        private IConfiguration _configuration;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthService(IHttpClientService httpService, IConfiguration configuration,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor)
        {
            _httpService = httpService;
            _configuration = configuration;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResult> Register(UserRegistrationModel Userregister)
        {
             var result = await _httpService.Post(Userregister, $"{Apiroute()}Auth/register/");
            return await _httpService.DeserializeAsync<RegisterResult>(result);

        }

        public async Task<LoginResult> Login(LogIn Userlogin)
        {
            LoginResult result = new();
            try
            {
               var response = await _httpService.Post(Userlogin, $"{Apiroute()}Auth/login/");
                result.Token = await response.Content.ReadAsStringAsync();

                 if (response == null) return new();
                if (response.IsSuccessStatusCode)
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);
                    httpContextAccessor?.HttpContext?.Request.Cookies.Append(new KeyValuePair<string, string>("jwt", result.Token ) );
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
                    result.Successful = true;
                    return result;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
        private string Apiroute()
        {
            var apiRoute = _configuration["Api:Apiroute"];
            if (apiRoute == null)
                return "https://localhost:7150/api/";
            return apiRoute;
        }
    }
}

