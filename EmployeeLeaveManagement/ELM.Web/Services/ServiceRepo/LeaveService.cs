using DTOs;
using EmpLeave.Web.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class LeaveService : ILeaveService
    {
        private HttpClient _httpService;
        private string controllerRoute = "https://localhost:7150/api/leave";
        public LeaveService(HttpClient httpService)
        {
            _httpService = httpService;
           
        }

        public async Task DeleteLeave(int id)
        {
            await _httpService.DeleteAsync($"{controllerRoute}/{id}");
        }

        public async Task<List<LeaveDto>> GetAllLeaves()
        {
            List<LeaveDto> respons = new();
            try
            {
                respons = await _httpService.GetFromJsonAsync<List<LeaveDto>>(controllerRoute);

            }
            catch (System.Exception)
            {
                respons = null;
            }
            return respons;
        }

        public async Task<LeaveDto> GetByIdCall(int id)
        {
            return await _httpService.GetFromJsonAsync<LeaveDto>(controllerRoute + "/GetById/" + id);
        }

        public async Task AddLeave(LeaveDto leaveDto)
        {
            await _httpService.PostAsJsonAsync<LeaveDto>(controllerRoute,leaveDto);

        }

        public  async Task EditLeave(LeaveDto leaveDto)
        {
            await _httpService.PutAsJsonAsync(controllerRoute, leaveDto);
        }
    }
}
