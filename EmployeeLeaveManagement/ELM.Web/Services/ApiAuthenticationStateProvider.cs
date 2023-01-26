﻿//using Microsoft.AspNetCore.Components.Authorization;
//using System.Security.Claims;
//using System.Text.Json;
//namespace RetailStoreManagement
//{
//    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
//    {
//        private readonly IHttpClientFactory _httpClient;
//        private readonly IHttpContextAccessor httpContextAccessor;

//        public ApiAuthenticationStateProvider(IHttpClientFactory httpClient,IHttpContextAccessor HttpContextAccessor)
//        {
//            _httpClient = httpClient;
//            httpContextAccessor = HttpContextAccessor;
//        }
//        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            try
//            {
//                var savedToken = httpContextAccessor.HttpContext.Request.Cookies["jwt"];

//                if (string.IsNullOrWhiteSpace(savedToken))
//                {
//                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//                }
//                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
           
//        }

//        public void MarkUserAsAuthenticated(string token)
//        {
//            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
//            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
//            NotifyAuthenticationStateChanged(authState);
//        }

//        public void MarkUserAsLoggedOut()
//        {
//            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
//            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
//            NotifyAuthenticationStateChanged(authState);
//        }

//        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
//        {
//            var claims = new List<Claim>();
//            var payload = jwt.Split('.')[1];
//            var jsonBytes = ParseBase64WithoutPadding(payload);
//            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

//            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

//            if (roles != null)
//            {
//                if (roles.ToString().Trim().StartsWith("["))
//                {
//                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

//                    foreach (var parsedRole in parsedRoles)
//                    {
//                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
//                    }
//                }
//                else
//                {
//                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
//                }

//                keyValuePairs.Remove(ClaimTypes.Role);
//            }

//            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

//            return claims;
//        }

//        private byte[] ParseBase64WithoutPadding(string base64)
//        {
//            switch (base64.Length % 4)
//            {
//                case 2: base64 += "=="; break;
//                case 3: base64 += "="; break;
//            }
//            return Convert.FromBase64String(base64);
//        }
//    }
//}
