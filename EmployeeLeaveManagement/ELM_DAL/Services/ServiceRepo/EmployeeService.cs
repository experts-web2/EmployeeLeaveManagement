using DTOs;
using EmpLeave.Web.Services.Interface;
using System.Text;
using Newtonsoft.Json;
using ELM.Helper;
using DomainEntity.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpService = httpClient;
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");
        }
        public async Task AddEmployee(EmployeeDto employeeDto)
        {
            await _httpService.PostAsJsonAsync($"{Apiroute()}employee", employeeDto);
        }
        public async Task<Response<EmployeeDto>> GetAllEmployee(Pager paging)
        {
            Response<EmployeeDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync($"{Apiroute()}employee/getall", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<EmployeeDto>();
                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<EmployeeDto>>(result);
                return responseDto;
            }
            catch (Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<EmployeeDto>();
        }
        public async Task UpdateEmployee(EmployeeDto employeeDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}employee", employeeDto);
        }
        public async Task DeleteEmployeebyId(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}employee/{id}");
        }
        public async Task<EmployeeDto> GetEmployeebyId(int id)
        {
            return await _httpService.GetFromJsonAsync<EmployeeDto>($"{Apiroute()}employee/GetById/{id}");
        }
        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _httpService.GetFromJsonAsync<List<Employee>>($"{Apiroute()}employee/GetAllEmployees");
        }
        private string Apiroute()
        {
            var ApiRoute = _configuration["Api:Apiroute"];
            return ApiRoute;
        }
    }
}
