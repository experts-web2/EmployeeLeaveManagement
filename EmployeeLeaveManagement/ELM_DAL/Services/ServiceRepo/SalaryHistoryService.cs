using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
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
    public class SalaryHistoryService : ServiceBase,ISalaryHistory
    {
        private HttpClient _httpService;
        private IHttpClientFactory _clientFactory;
        public SalaryHistoryService(HttpClient httpService,
            IConfiguration _configuration,
            IHttpClientFactory httpClientFactory,
            IJSRuntime jSRuntime,AuthenticationStateProvider authenticationStateProvider):base(httpService, _configuration, jSRuntime, authenticationStateProvider)
        {
         
            _httpService = httpService;
            _clientFactory = httpClientFactory;
        }
        public async Task AddSalary(SalaryHistoryDto salaryHistoryDto)
        {
            var response= await _httpService.PostAsJsonAsync($"{Apiroute()}SalaryHistory/AddSalary", salaryHistoryDto);
        }

        public async Task<Response<SalaryHistoryDto>> GetSalaries(Pager paging)
        {
            Response<SalaryHistoryDto> responseDto = new();
            try
            {
                await SetToken();
                _httpService = _clientFactory.CreateClient("api");
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                HttpResponseMessage response;
                if (user.IsInRole("Admin"))
                    response = await _httpService.PostAsync($"{Apiroute()}SalaryHistory/getSalaries", Content);
                else
                    response = await _httpService.PostAsync($"{Apiroute()}SalaryHistory/GetSalariesForUser/{int.Parse(employeeId)}",Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<SalaryHistoryDto>();
                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<SalaryHistoryDto>>(result);
                return responseDto;
            }
            catch (Exception)
            {

                responseDto.DataList = null;
            }
            return new Response<SalaryHistoryDto>();
        }

        public async Task<SalaryHistoryDto> GetSalaryById(int id)
        {
            try
            {
                await SetToken();
                return await _httpService.GetFromJsonAsync<SalaryHistoryDto>($"{Apiroute()}SalaryHistory/GetById/{id}");
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public async Task DeleteSalary(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}SalaryHistory/{id}");
        }
        public async Task UpdateSalary(SalaryHistoryDto salaryHistoryDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}SalaryHistory/EditSalary", salaryHistoryDto);
        }
    }
}
