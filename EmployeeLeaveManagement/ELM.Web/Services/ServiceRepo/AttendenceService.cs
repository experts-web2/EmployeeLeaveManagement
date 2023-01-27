using DTOs;
using ELM.Web.Services.Interface;

namespace ELM.Web.Services.ServiceRepo
{
    public class AttendenceService : IAttendenceService

    {
        private HttpClient _httpService;
        private string controllerRoute = "https://localhost:7150/api/attendenceapi";
        public AttendenceService(HttpClient httpService)
        {
            _httpService = httpService;
            
        }

        public Task DeleteCall(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AttendenceDto>> GetAttendences()
        {
         
            List<AttendenceDto> respons = new();
            try
            {
                respons = await _httpService.GetFromJsonAsync<List<AttendenceDto>>(controllerRoute);
                return respons;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
            
        }

        public Task<LeaveDto> GetByIdCall(int id)
        {
            throw new NotImplementedException();
        }

        public async Task PostCall(AttendenceDto attendenceDto)
        {
            await _httpService.PostAsJsonAsync(controllerRoute, attendenceDto);
        }

        public Task UpdateCall(LeaveDto leaveDto)
        {
            throw new NotImplementedException();
        }
    }
}
