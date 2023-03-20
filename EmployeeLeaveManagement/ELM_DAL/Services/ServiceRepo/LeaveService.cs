using DTOs;
using ELM.Helper;
using EmpLeave.Web.Services.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ELM_DAL.Services.ServiceRepo
{
    public class LeaveService : ILeaveService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        public LeaveService(HttpClient httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");
        }
        public async Task DeleteLeave(int id)
        {
            await _httpService.DeleteAsync($"{Apiroute()}leave/{id}");
        }
        public async Task<Response<LeaveDto>> GetAllLeaves(Pager Paging)
        {
            Response<LeaveDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(Paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync($"{Apiroute()}leave/getall", Content);
                if (!response.IsSuccessStatusCode)
                    return new Response<LeaveDto>();

                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<LeaveDto>>(result);
                return responseDto;
            }
            catch (System.Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<LeaveDto>();
        }
        public async Task<LeaveDto> GetByIdCall(int id)
        {
            try
            {
                var response = await _httpService.GetFromJsonAsync<LeaveDto>($"{Apiroute()}leave/GetById/{id}");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AddLeave(LeaveDto leaveDto)
        {
            await _httpService.PostAsJsonAsync<LeaveDto>($"{Apiroute()}leave", leaveDto);
        }
        public async Task EditLeave(LeaveDto leaveDto)
        {
            await _httpService.PutAsJsonAsync($"{Apiroute()}leave", leaveDto);
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
