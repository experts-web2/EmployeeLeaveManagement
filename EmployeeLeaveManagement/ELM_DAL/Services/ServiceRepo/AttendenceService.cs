using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Shared;
using ELM.Web.Services.Interface;
using ELM_DAL.Services;
using EmpLeave.Api.Controllers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace ELM.Web.Services.ServiceRepo
{
    public class AttendenceService : ServiceBase, IAttendenceService

    {
        public AttendenceService(HttpClient httpService,
            IConfiguration configuration,
            IHttpClientFactory clientFactory,
            AuthenticationStateProvider authenticationStateProvider,
            IJSRuntime jsRuntime = null) : base(httpService, configuration, jsRuntime, authenticationStateProvider)
        {
        }

        public async Task<Response<AttendenceDto>> GetAttendences(Pager paging)
        {
            Response<AttendenceDto> responseDto = new();
            try
            {
                await SetToken();
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                HttpResponseMessage response;
                if (user.IsInRole("Admin"))
                    response = await _httpService.PostAsync($"{Apiroute()}AttendenceApi/GetAllAttendences", Content);
                else
                    response = await _httpService.PostAsync($"{Apiroute()}AttendenceApi/GetAttendencesByEmployeeId/{int.Parse(employeeId)}", Content);
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
            try
            {
                await SetToken();
                var response = await _httpService.PostAsJsonAsync($"{Apiroute()}AttendenceApi/AddAttendence", attendenceDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteAttendence(int id)
        {
            try
            {
                await _httpService.DeleteAsync($"{Apiroute()}AttendenceApi/{id}");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateAttendence(AttendenceDto attendenceDto)
        {
            try
            {
                await SetToken();
                await _httpService.PutAsJsonAsync($"{Apiroute()}AttendenceApi", attendenceDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<AttendenceDto> GetByID(int value)
        {
            try
            {
                await SetToken();
                var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetById?id={value}");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<AttendenceDto> GetAttendenceByEmployeeId(int value, DateTime attendenceDate)
        {
            try
            {
                await SetToken();
                var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetAttendenceByEmployeeId?id={value}&attendenceDate={attendenceDate}");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async  Task<AttendenceDto> GetAttendenceByAlertDateAndEmployeeId(DateTime alertDate, int employeeId)
        {
            await SetToken();
             var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetAttendenceByAlertDateAndEmployeeId?alertDate={alertDate}&employeeId={employeeId}");
            return result;

        }

        public async Task<List<AttendenceDto>> GetAttendencesWithoutPagination(string employeeId,ClaimsPrincipal user)
        {
            try
            {
                await SetToken();
                List<AttendenceDto> response = new List<AttendenceDto>();
                if (user.IsInRole("Admin"))
                {
                    response = await _httpService.GetFromJsonAsync<List<AttendenceDto>>($"{Apiroute()}AttendenceApi/GetAllAttendencesWithoutPaging");
                    return response;
                }
                else
                {
                    response = await _httpService.GetFromJsonAsync<List<AttendenceDto>>($"{Apiroute()}AttendenceApi/GetAttendencesByEmployeeId/{int.Parse(employeeId)}");
                    return response;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
