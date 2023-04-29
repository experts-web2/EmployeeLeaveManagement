using DomainEntity.Models;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace ELM_DAL.Services.ServiceRepo;

public sealed class AlertService : ServiceBase, IAlertService
{

    public AlertService(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime, AuthenticationStateProvider authenticationStateProvider) : base(httpService, configuration, jSRuntime, authenticationStateProvider)
    {}

    public async Task<Response<Alert>> GetAlerts(Pager paging)
    {
        Response<Alert> responseDto = new();
        try
        {
            await SetToken();
            AlertService hjh = new AlertService(_httpService,_configuration,_jsRuntime,_authenticationStateProvider);
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
    public async Task<List<Alert>> GetAllAlertsByEmployeeId(int id)
    {
        try
        {
            await SetToken();
            var response = _httpService.GetFromJsonAsync<List<Alert>>($"{Apiroute()}Alert/GetAlertsByEmployeeId/{id}");
            return response.GetAwaiter().GetResult();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task DeleteAlert(int id, List<DateTime> attendenceDates)
    {
        try
        {
            await SetToken();
            string data = JsonConvert.SerializeObject(attendenceDates);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpService.SendAsync(new HttpRequestMessage
            {
                Content = Content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Apiroute()}Alert/{id}")
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Alert> GetAlertById(int id)
    {
        try
        {
            await SetToken();
            var alert = await _httpService.GetFromJsonAsync<Alert>($"{Apiroute()}Alert/GetAlertById{id}");
            return alert;
        }
        catch (Exception)
        {

            throw;
        }
      
    }
    public async Task UpdateAlert(Alert alert)
    {
        try
        {
            await SetToken();
            await  _httpService.PutAsJsonAsync($"{Apiroute()}Alert/UpdateAlert",alert);

        }
        catch (Exception)
        {

            throw;
        }
    }
}

