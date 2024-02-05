using DomainEntity.Models;
using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ELM_DAL.Services.ServiceRepo
{
    public class DailyTaskService : ServiceBase, IDailyTasksService
    {
        private HttpClient _httpService;

        public DailyTaskService(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory) : base(httpService, configuration, jSRuntime, authenticationStateProvider)
        {
            _httpService = httpService;
        }
        public async Task<string> AddDailyTask(DailyTaskDto dailyTaskDto)
        {
            await SetToken();
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            dailyTaskDto.EmployeeId = int.Parse(employeeId);
            var response = await _httpService.PostAsJsonAsync($"{Apiroute()}DailyTask", dailyTaskDto);
            var taskResult = response.Content.ReadFromJsonAsync<DailyTaskDto>().Result;
            if (taskResult != null)
            {
                return "Task added succesfully";
            }
            return "Task not added";
           
        }

        public async Task<List<DailyTaskDto>> GetAllDailyTask()
        {
            await SetToken();
            return await _httpService.GetFromJsonAsync<List<DailyTaskDto>>($"{Apiroute()}DailyTask");
        }
    }
}
