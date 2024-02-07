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
        public async Task<string> AddDailyTimeSheet(DailyTimeSheetDto dailyTimeSheetDto)
        {
            await SetToken();
            var response =  await _httpService.PostAsJsonAsync($"{Apiroute()}DailyTimeSheet", dailyTimeSheetDto);
            var res = response.Content.ReadAsStringAsync().Result;
            if (response.Content.ReadAsStringAsync().Result == "")
            {
                return "Time Sheet already Created !!!";
            }
            else
                return "Time Sheet created";
        }

        public async Task<List<DailyTimeSheetDto>> GetAllDailyTimeSheet()
        {
            await SetToken();
            return  await _httpService.GetFromJsonAsync<List<DailyTimeSheetDto>>($"{Apiroute()}DailyTimeSheet");
        }
    }
}
