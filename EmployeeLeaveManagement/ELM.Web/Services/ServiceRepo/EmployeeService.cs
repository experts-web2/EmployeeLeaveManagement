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

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpService;
        HttpClient httpclient = new HttpClient();
        private  string controllerRoute = "https://localhost:7150/api/employee";
        public EmployeeService(HttpClient httpClient)
        {
            _httpService= httpClient;
        }
        public async Task PostCall(EmployeeDto employeeDto)
        {
             await _httpService.PostAsJsonAsync(controllerRoute, employeeDto);
            
        }
        public async Task<Response<EmployeeDto>> GetAllEmployee(Parameter parameter)
        {
            //  List<EmployeeDto> respons = new();
            Response<EmployeeDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(parameter);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await httpclient.PostAsync("Employee/getall", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<EmployeeDto>();

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = await _httpService.GetFromJsonAsync<List<EmployeeDto>>(controllerRoute);

            }
            catch (System.Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<EmployeeDto>();
        }
        public async Task UpdateCall(EmployeeDto employeeDto)
        {
            await _httpService.PutAsJsonAsync(controllerRoute, employeeDto);
        }
        public async Task DeleteCall(int id)
        {
            await _httpService.DeleteAsync($"{controllerRoute}/{id}");
        }
        public async Task<EmployeeDto> GetByIdCall(int id)
        {
          return  await _httpService.GetFromJsonAsync<EmployeeDto>(controllerRoute+"/GetById/"+id);
        }
    }
}
