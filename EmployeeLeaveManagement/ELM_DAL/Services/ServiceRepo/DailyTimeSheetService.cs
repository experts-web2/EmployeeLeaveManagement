using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ELM_DAL.Services.ServiceRepo
{
    public class DailyTimeSheetService : ServiceBase,IDailyTimeSheetService
    {
        public DailyTimeSheetService(HttpClient httpService,
           IConfiguration configuration,
           IJSRuntime jsRunTime, AuthenticationStateProvider authenticationStateProvider) : base(httpService, configuration, jsRunTime, authenticationStateProvider)
        {
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");

        }
        public async Task AddDailyTimeSheet(DailyTimeSheetDto dailyTimeSheetDto)
        {
            await SetToken();
            await _httpService.PostAsJsonAsync($"{Apiroute()}DailyTimeSheet", dailyTimeSheetDto);
        }

        public Task GetAllDailyTimeSheet()
        {
            throw new NotImplementedException();
        }
    }
}
