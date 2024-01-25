using DomainEntity.Models;
using DTOs;
using ELM.Helper;
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
    public class LoanService : ServiceBase, ILoanService
    {
        public LoanService(HttpClient httpService,
            IConfiguration configuration,
            IJSRuntime jsRunTime, AuthenticationStateProvider authenticationStateProvider) : base(httpService, configuration, jsRunTime, authenticationStateProvider)
        {
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");

        }

        public async Task AddLoan(LoanDto loanDto)
        {
            await SetToken();
            await _httpService.PostAsJsonAsync<LoanDto>($"{Apiroute()}Loan", loanDto);
        }

      
        public Task DeleteLoan(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LoanDto> GetLoanById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoanDto> GetLoanByEmployeeId(int employeeId)
        {
            var listofLoan = await GetLoans();
           return listofLoan.Where(x => x.EmployeeId == employeeId).FirstOrDefault();

        }

        public async Task<List<LoanDto>> GetLoans()
        {
           await SetToken();
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            
            if (user.IsInRole("Admin"))
            {
                return await _httpService.GetFromJsonAsync<List<LoanDto>>($"{Apiroute()}Loan");
            }
            return await _httpService.GetFromJsonAsync<List<LoanDto>>($"{Apiroute()}Loan/GetLoanByEmployeeId");
        }

        public Task UpdateLoan(LoanDto loanDto)
        {
            throw new NotImplementedException();
        }
    }
}
