﻿using DTOs;
using ELM.Helper;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace ELM_DAL.Services.ServiceRepo
{
    public class LeaveService : ILeaveService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        private IJSRuntime _jsRunTime;
        private AuthenticationStateProvider _authenticationStateProvider;
        public LeaveService(HttpClient httpService,
            IConfiguration configuration,
            IJSRuntime jsRunTime,AuthenticationStateProvider authenticationStateProvider)
        {
            _httpService = httpService;
            _configuration = configuration;
            _jsRunTime = jsRunTime;
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task DeleteLeave(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}leave/{id}");
        }
        public async Task<Response<LeaveDto>> GetAllLeaves(Pager Paging)
        {
            Response<LeaveDto> responseDto = new();
            try
            {
                var token = await _jsRunTime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                string data = JsonConvert.SerializeObject(Paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                HttpResponseMessage response;
                if (user.IsInRole("Admin"))
                    response = await _httpService.PostAsync($"{Apiroute()}leave/getall", Content);
                else
                    response = await _httpService.PostAsync($"{Apiroute()}leave/GetLeavesByEmployeeId/{int.Parse(employeeId)}", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<LeaveDto>();

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<LeaveDto>>(result);
                return responseDto;
            }
            catch (System.Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<LeaveDto>();
        }
        public async Task<LeaveDto> GetByIdCall(int id)
        {
            try
            {
                var response = await _httpService.GetFromJsonAsync<LeaveDto>($"{Apiroute()}leave/GetById/{id}");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AddLeave(LeaveDto leaveDto)
        {
            await _httpService.PostAsJsonAsync<LeaveDto>($"{Apiroute()}leave", leaveDto);
        }
        public async Task EditLeave(LeaveDto leaveDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}leave", leaveDto);
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
