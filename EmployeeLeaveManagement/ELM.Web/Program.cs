using Blazored.LocalStorage;
using ELM.Shared;
using ELM.Web.Data;
using ELM.Web.Services.Interface;
using ELM.Web.Services.ServiceRepo;
using ELM_DAL.Services.Interface;
using ELM_DAL.Services.ServiceRepo;
using EmpLeave.Api.Controllers;
using EmpLeave.Web.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthorizationCore();
//builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IAttendenceService, AttendenceService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ISalaryHistory, SalaryHistoryService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient("api", o =>
{
     o.BaseAddress = new Uri("https://localhost:7150/");
    var jsRuntime = builder.Services.BuildServiceProvider().GetService<IHttpContextAccessor>();

    var token = jsRuntime.HttpContext.Request.Cookies["jwt"];
    //var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer",$"{token}");//await _localStorage.GetItemAsync<string>("authToken"));

});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
