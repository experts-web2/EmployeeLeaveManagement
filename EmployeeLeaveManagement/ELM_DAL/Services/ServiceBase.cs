using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace ELM_DAL.Services;

public abstract class ServiceBase
{
    private protected readonly HttpClient _httpService;
    private protected readonly IConfiguration _configuration;
    private protected readonly IJSRuntime _jsRuntime;
    private protected readonly AuthenticationStateProvider _authenticationStateProvider;

    public ServiceBase(HttpClient httpService, IConfiguration configuration, IJSRuntime jSRuntime, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpService = httpService;
        _configuration = configuration;
        _jsRuntime = jSRuntime;
        _authenticationStateProvider = authenticationStateProvider;
    }

    private protected string Apiroute()
    {
        var apiRoute = _configuration["Api:Apiroute"];
        if (apiRoute == null)
            return "https://localhost:7150/api/";

        return apiRoute;
    }

    private protected async Task SetToken()
     {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
            token = token?.Replace("\"", "");
            _httpService.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        }
        catch (Exception)
        {
            throw;
        }
    }
}