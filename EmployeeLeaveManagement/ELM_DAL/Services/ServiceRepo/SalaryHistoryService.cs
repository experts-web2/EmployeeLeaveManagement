using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ELM_DAL.Services.ServiceRepo
{
    public class SalaryHistoryService:ISalaryHistory
    {
        private HttpClient _httpService;
        private IConfiguration configuration;
        private IHttpClientFactory _clientFactory;
        private readonly IJSRuntime _jsRuntime;
        public SalaryHistoryService(HttpClient httpService,IConfiguration _configuration,IHttpClientFactory httpClientFactory,IJSRuntime jSRuntime)
        {
            configuration = _configuration;
            _httpService = httpService;
            _clientFactory = httpClientFactory;
            _jsRuntime = jSRuntime;
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

                _httpService = _clientFactory.CreateClient("api");
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync($"{Apiroute()}SalaryHistory/getSalaries", Content);
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
            return await _httpService.GetFromJsonAsync<SalaryHistoryDto>($"{Apiroute()}SalaryHistory/GetById/{id}");
        }
        public async Task DeleteSalary(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}SalaryHistory/{id}");
        }
        public async Task UpdateSalary(SalaryHistoryDto salaryHistoryDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}SalaryHistory/EditSalary", salaryHistoryDto);
        }
        private string Apiroute()
        {
            var apiRoute = configuration["Api:Apiroute"];
            if (apiRoute == null)
                return "https://localhost:7150/api/";
            return apiRoute;
        }
    }
}
