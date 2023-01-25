using DTOs;
using EmpLeave.Web.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpService;
        private  string controllerRoute = "https://localhost:7150/api/employee";
        public EmployeeService(HttpClient httpClient)
        {
            _httpService= httpClient;
        }
        public async Task PostCall(EmployeeDto employeeDto)
        {
             await _httpService.PostAsJsonAsync(controllerRoute, employeeDto);
            
        }
        public async Task<List<EmployeeDto>> GetAllEmployee()
        {
            List<EmployeeDto> respons = new();
            try
            {
                 respons = await _httpService.GetFromJsonAsync<List<EmployeeDto>>(controllerRoute);

            }
            catch (System.Exception)
            {
                respons = null;
            }
            return respons;
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
