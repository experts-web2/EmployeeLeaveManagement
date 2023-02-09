using DTOs;
using ELM.Web.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace ELM.Web.Services.ServiceRepo
{
    public class AttendenceService : IAttendenceService

    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        public AttendenceService(HttpClient httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
        }
        public async Task<List<AttendenceDto>> GetAttendences()
        {
            List<AttendenceDto> response = new();
            try
            {
                response = await _httpService.GetFromJsonAsync<List<AttendenceDto>>($"{Apiroute()}AttendenceApi/GetAllAttendences");
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task AddAttendence(AttendenceDto attendenceDto)
        {
            var response = await _httpService.PostAsJsonAsync($"{Apiroute()}AttendenceApi/AddAttendence", attendenceDto);
        }
        public async Task DeleteAttendence(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}AttendenceApi/{id}");
        }
        public async Task UpdateAttendence(AttendenceDto attendenceDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}AttendenceApi", attendenceDto);
        }
        public async Task<AttendenceDto> GetByID(int value)
        {
            return await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetById/{value}");
        }
        private string Apiroute()
        {
            var ApiRoute = _configuration["Api:Apiroute"];
            return ApiRoute;
        }
    }
}
