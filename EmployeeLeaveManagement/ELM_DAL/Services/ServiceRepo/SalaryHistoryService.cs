using DTOs;
using ELM.Web.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace ELM.Web.Services.ServiceRepo
{
    public class SalaryHistoryService:ISalaryHistory
    {
        private HttpClient _httpService;
        private IConfiguration configuration;
        public SalaryHistoryService(HttpClient httpService,IConfiguration _configuration)
        {
            configuration = _configuration;
            _httpService = httpService;
        }
        public async Task AddSalary(SalaryHistoryDto salaryHistoryDto)
        {
            var response= await _httpService.PostAsJsonAsync($"{Apiroute()}SalaryHistory/AddSalary", salaryHistoryDto);
        }

        public async Task<List<SalaryHistoryDto>> GetSalaries()
        {
            return await _httpService.GetFromJsonAsync<List<SalaryHistoryDto>>($"{Apiroute()}SalaryHistory/GetSalaries");
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
