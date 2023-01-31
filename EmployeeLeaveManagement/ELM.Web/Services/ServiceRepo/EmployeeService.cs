using DomainEntity.Pagination;
using DTOs;
using ELM.Web.Helper;
using EmpLeave.Web.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pager = ELM.Web.Helper.Pager;

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
        public async Task PostCall(EmployeeDto employeeDto)
        {
             await _httpService.PostAsJsonAsync(_httpService.BaseAddress, employeeDto);
            
        }
        public async Task<Response<EmployeeDto>> GetAllEmployee(Parameter parameter)
        {
            Response<EmployeeDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(parameter);
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
        public async Task UpdateCall(EmployeeDto employeeDto)
        {
            await _httpService.PutAsJsonAsync(_httpService.BaseAddress, employeeDto);
        }
        public async Task DeleteCall(int id)
        {
            await _httpService.DeleteAsync($"{_httpService.BaseAddress}/{id}");
        }
        public async Task<EmployeeDto> GetByIdCall(int id)
        {
          return  await _httpService.GetFromJsonAsync<EmployeeDto>(_httpService.BaseAddress + "/GetById/"+id);
        }
    }
}
