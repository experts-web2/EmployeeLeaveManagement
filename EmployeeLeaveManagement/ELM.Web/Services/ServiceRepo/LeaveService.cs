using DTOs;
using ELM.Helper.SupportFiles;
using ELM.Web.Helper;
using EmpLeave.Web.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmpLeave.Web.Services.ServiceRepo
{
    public class LeaveService : ILeaveService
    {
        private HttpClient _httpService;

        public LeaveService(HttpClient httpService)
        {
            _httpService = httpService;
            _httpService.BaseAddress = new Uri("https://localhost:7150/api/");
            _httpService.DefaultRequestHeaders.Add("Accept", "Application/json");
        }
        public async Task DeleteCall(int id)
        {
            await _httpService.DeleteAsync($"{_httpService.BaseAddress}/{id}");
        }
        public async Task<Response<LeaveDto>> GetAllLeaves(Pager Paging)
        {
            Response<LeaveDto> responseDto = new();
            try
            {
                string data = JsonConvert.SerializeObject(Paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpService.PostAsync("Leave/getall", Content);
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
                // respons = await _httpService.GetFromJsonAsync<List<LeaveDto>>(_httpService.BaseAddress);

            }
            catch (System.Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<LeaveDto>();
        }

        public async Task<LeaveDto> GetByIdCall(int id)
        {
            return await _httpService.GetFromJsonAsync<LeaveDto>(_httpService.BaseAddress + "/GetById/" + id);
        }

        public async Task PostCall(LeaveDto leaveDto)
        {
            await _httpService.PostAsJsonAsync<LeaveDto>(_httpService.BaseAddress, leaveDto);

        }

        public async Task UpdateCall(LeaveDto leaveDto)
        {
            await _httpService.PutAsJsonAsync(_httpService.BaseAddress, leaveDto);
        }
    }
}
