using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.ServiceRepo
{
    public class AlertService : IAlertService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        private readonly IJSRuntime _jsRuntime;
        public AlertService(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime)
        {
            _httpService = httpService;
            _configuration = configuration;
            _jsRuntime = jSRuntime;
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
                var response = await _httpService.PostAsync($"{Apiroute()}Alert/GetAlerts", Content);
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
    }
}

