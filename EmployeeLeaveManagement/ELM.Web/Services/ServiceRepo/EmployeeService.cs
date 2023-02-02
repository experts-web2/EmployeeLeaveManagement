using DTOs;
using EmpLeave.Web.Services.Interface;
using System.Text;
using Newtonsoft.Json;
using ELM.Helper;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpService;
     
        public EmployeeService(HttpClient httpClient)
        {
            _httpService= httpClient;
            _httpService.BaseAddress = new Uri("https://localhost:7150/api/");
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");
        }
        public async Task AddEmployee(EmployeeDto employeeDto)
        {
             await _httpService.PostAsJsonAsync(_httpService.BaseAddress, employeeDto);
            
        }
        public async Task<Response<EmployeeDto>> GetAllEmployee(Pager paging)
        {
            Response<EmployeeDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync("Employee/getall", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<EmployeeDto>();

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList =  JsonConvert.DeserializeObject<List<EmployeeDto>>(result);
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
            await _httpService.PutAsJsonAsync(_httpService.BaseAddress, employeeDto);
        }
        public async Task DeleteEmployeebyId(int id)
        {
            await _httpService.DeleteAsync($"{_httpService.BaseAddress}/{id}");
        }
        public async Task<EmployeeDto> GetEmployeebyId(int id)
        {
          return  await _httpService.GetFromJsonAsync<EmployeeDto>(_httpService.BaseAddress + "/GetById/"+id);
        }
    }
}
