using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.ServiceRepo
{
    public class SalaryService : ServiceBase, ISalaryService
    {
        private IHttpClientFactory _clientFactory;

        public SalaryService(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory) : base(httpService, configuration, jSRuntime, authenticationStateProvider)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task AddSalary()
        {
            await SetToken();
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpService.PostAsJsonAsync($"{Apiroute()}Salary", employeeId);
        }

        public async Task<List<SalaryDto>?> GetSalaries()
        {

            try
            {
               
                await SetToken();               
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
               
                if (user.IsInRole("Admin"))
                    return await _httpService.GetFromJsonAsync<List<SalaryDto>>($"{Apiroute()}Salary");
                else
                    return await _httpService.GetFromJsonAsync<List<SalaryDto>>($"{Apiroute()}Salary/GetEmployeeSalary/{int.Parse(employeeId)}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
