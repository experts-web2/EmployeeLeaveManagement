using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.ServiceRepo
{
    public class LoanInstallmentHistoryService : ServiceBase, ILoanInstallmentHistoryService
    {

        public LoanInstallmentHistoryService(HttpClient httpService,
             IConfiguration configuration,
             IJSRuntime jsRunTime, AuthenticationStateProvider authenticationStateProvider) : base(httpService, configuration, jsRunTime, authenticationStateProvider)
        {
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");

        }
        public async Task<List<LoanInstallmentHistoryDto>> GetEmployeeLoanInstallmentHistory()
        {
            await SetToken();
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var response = await _httpService.GetFromJsonAsync<List<LoanInstallmentHistoryDto>>($"{Apiroute()}LoanInstallmentHistory/GetLoanInstallmentById/{employeeId}");
            if (response != null)
            {
                return response;
            }
            return new List<LoanInstallmentHistoryDto>();
        }
    }
}
