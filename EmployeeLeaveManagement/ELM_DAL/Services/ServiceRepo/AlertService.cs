using DomainEntity.Models;
using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.ServiceRepo
{
    public class AlertService : IAlertService
    {
        private HttpClient _httpService;
        private IConfiguration _configuration;
        public AlertService(HttpClient httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
        }

        public async Task<List<Alert>> GetAlerts()
        {
            List<Alert> response = new();
            try
            {
                response = await _httpService.GetFromJsonAsync<List<Alert>>($"{Apiroute()}Alert");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
