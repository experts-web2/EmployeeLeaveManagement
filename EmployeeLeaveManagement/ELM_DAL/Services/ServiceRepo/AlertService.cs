using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.ServiceRepo
{
    public class AlertService : IAlertService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AlertService(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime,AuthenticationStateProvider authenticationStateProvider)
        {
            _httpService = httpService;
            _configuration = configuration;
            _jsRuntime = jSRuntime;
            _authenticationStateProvider = authenticationStateProvider;
            
        }

        public async Task<Response<Alert>> GetAlerts(Pager paging)
        {
            Response<Alert> responseDto = new();
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                string data = JsonConvert.SerializeObject(paging);
                StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                HttpResponseMessage response;
                if (user.IsInRole("Admin"))
                {
                    response = await _httpService.PostAsync($"{Apiroute()}Alert/GetAlerts", Content);
                }
                else
                    response = await _httpService.PostAsJsonAsync($"{Apiroute()}Alert/GetAlertsByEmployeeId/{int.Parse(employeeId)}", Content);
                
                if (!response.IsSuccessStatusCode)
                    return new Response<Alert>();
                if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
                {
                    var metadata = keys.FirstOrDefault();
                    responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
                }
                string result = await response.Content.ReadAsStringAsync();
                responseDto.DataList = JsonConvert.DeserializeObject<List<Alert>>(result);
                return responseDto;
            }
            catch (Exception)
            {
                responseDto.DataList = null;
            }
            return new Response<Alert>();
        }
        //public async Task<Response<Alert>>GetAlert(Pager paging)
        //{
        //    Response<Alert> responseDto = new();
        //    var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
        //    token = token?.Replace("\"", "");
        //    _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //    string data = JsonConvert.SerializeObject(paging);
        //    StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
        //    var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        //    var user = authState.User;
        //    string employeeId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var response = await _httpService.PostAsJsonAsync($"{Apiroute()}Alert/GetAlertsByEmployeeId/{int.Parse(employeeId)}", Content);

        //    if (!response.IsSuccessStatusCode)
        //        return new Response<Alert>();
        //    if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> keys))
        //    {
        //        var metadata = keys.FirstOrDefault();
        //        responseDto.Pager = JsonConvert.DeserializeObject<Pager>(metadata);
        //    }
        //    string result = await response.Content.ReadAsStringAsync();
        //    responseDto.DataList = JsonConvert.DeserializeObject<List<Alert>>(result);
        //    return responseDto;

        //}
        //public async Task<Alert?> ShowAlerts()
        //{
        //    try
        //    {
        //        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
        //        token = token?.Replace("\"", "");
        //        _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        //        var response = await _httpService.GetFromJsonAsync<Alert?>($"{Apiroute()}Alert/ShowAlerts");
        //        return response;

        //    }
        //    catch (Exception)
        //    {

        //        throw;


        //    }
        //}
        private string Apiroute()
        {
            var apiRoute = _configuration["Api:Apiroute"];
            if (apiRoute == null)
                return "https://localhost:7150/api/";
            return apiRoute;
        }

        public async Task DeleteAlert(int? id)
        {
            try
            {

                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
                token = token?.Replace("\"", "");
                _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var response = await _httpService.DeleteAsync($"{Apiroute()}Alert/{id}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

