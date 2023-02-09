using DTOs;
using ELM.Web.Services.Interface;

namespace ELM.Web.Services.ServiceRepo
{
    public class SalaryHistoryService:ISalaryHistory
    {
        private HttpClient _httpService;
        private string controllerRoute = "https://localhost:7150/api/SalaryHistory";
        public SalaryHistoryService(HttpClient httpService)
        {
            _httpService = httpService;
        }
        public async Task AddSalary(SalaryHistoryDto salaryHistoryDto)
        {
            var response= await _httpService.PostAsJsonAsync($"{controllerRoute}/AddSalary", salaryHistoryDto);
        }
        public async Task<SalaryHistoryDto> GetSalaryById(int id)
        {
            return await _httpService.GetFromJsonAsync<SalaryHistoryDto>($"{_httpService.BaseAddress}/GetById/{id}");
        }
    }
}
