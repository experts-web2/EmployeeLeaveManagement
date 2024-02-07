using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;

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

        public async Task<Response<DailyTimeSheetDto>> GetDailyTimeSheetWithFilter(Pager paging)
        {
            Response<DailyTimeSheetDto> responseDto = new();

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpResponseMessage response;
            if (user.IsInRole("Admin"))
            {
                 response = await _httpService.PostAsJsonAsync($"{Apiroute()}DailyTimeSheet/DailyTimeSheetwithFilter", paging);
            }
            else
                response = await _httpService.PostAsJsonAsync($"{Apiroute()}DailyTimeSheet/DailyTimeSheetwithFilterByEmpId/{employeeId}", paging);
            if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
            {
                var metadata = keys.FirstOrDefault();
                responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
            }

            var result = await response.Content.ReadAsStringAsync();
            responseDto.DataList = JsonConvert.DeserializeObject<List<DailyTimeSheetDto>>(result);
            return responseDto;
         
        }
    }
}
