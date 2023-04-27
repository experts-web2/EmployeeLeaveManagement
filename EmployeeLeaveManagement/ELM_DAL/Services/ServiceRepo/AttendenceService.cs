using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Shared;
using ELM.Web.Services.Interface;
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
    public class AttendenceService : IAttendenceService

    {
        private HttpClient _httpService;
        private IHttpClientFactory _clientFactory;
        private readonly IJSRuntime jsRuntime;


        private AuthenticationStateProvider _authenticationStateProvider;
        private IConfiguration _configuration;
        public AttendenceService(HttpClient httpService,
            IConfiguration configuration,
            IHttpClientFactory clientFactory,
            AuthenticationStateProvider authenticationStateProvider,
            IJSRuntime jsRuntime = null)
        {
            _httpService = httpService;
            _configuration = configuration;
            _clientFactory = clientFactory;
            this.jsRuntime = jsRuntime;
            _authenticationStateProvider=authenticationStateProvider;
        }

        public async Task<Response<AttendenceDto>> GetAttendences(Pager paging)
        {
            Response<AttendenceDto> responseDto = new();
            try
            {
                _httpService = _clientFactory.CreateClient("api");
                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                HttpResponseMessage response;
                if (user.IsInRole("Admin"))
                    response = await _httpService.PostAsync($"{Apiroute()}AttendenceApi/GetAllAttendences", Content);
                else
                    response = await _httpService.GetAsync($"{Apiroute()}AttendenceApi/GetAttendencesByEmployeeId/{int.Parse(employeeId)}");
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

                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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

                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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
                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetById?id={value}");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<AttendenceDto> GetAttendenceByEmployeeId(int value)
        {
            try
            {
                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetAttendenceByEmployeeId?id={value}");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string Apiroute()
        {
            var apiRoute = _configuration["Api:Apiroute"];
            if (apiRoute == null)
                return "https://localhost:7150/api/";
            return apiRoute;
        }

        public async  Task<AttendenceDto> GetAttendenceByAlertDateAndEmployeeId(DateTime alertDate, int employeeId)
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
            token = token?.Replace("\"", "");
            _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
             var result = await _httpService.GetFromJsonAsync<AttendenceDto>($"{Apiroute()}AttendenceApi/GetAttendenceByAlertDateAndEmployeeId?alertDate={alertDate}&employeeId={employeeId}");
            return result;

        }
    }
}
