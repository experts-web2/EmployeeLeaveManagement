using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

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
        public async Task<Response<AttendenceDto>> GetAttendences(Pager paging)
        {
            Response<AttendenceDto> responseDto = new();
            try
            {

                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync($"{Apiroute()}AttendenceApi/GetAllAttendences", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<AttendenceDto>();
                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<AttendenceDto>>(result);
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.DataList = null;
            }
            return new Response<AttendenceDto>();
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
            var apiRoute = _configuration["Api:Apiroute"];
            if (apiRoute == null)
                return "https://localhost:7150/api/";
            return apiRoute;
        }
    }
}
