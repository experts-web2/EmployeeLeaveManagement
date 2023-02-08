using ELM.Web.Data;
using ELM.Web.Services.Interface;
using ELM.Web.Services.ServiceRepo;
using EmpLeave.Web.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
using Microsoft.AspNetCore.Components.Authorization;
using RetailStoreManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthorizationCore();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IAttendenceService, AttendenceService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ISalaryHistory, SalaryHistoryService>();
builder.Services.AddHttpClient("api", o =>
{
    o.BaseAddress = new Uri("https://localhost:7150/api/");
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
