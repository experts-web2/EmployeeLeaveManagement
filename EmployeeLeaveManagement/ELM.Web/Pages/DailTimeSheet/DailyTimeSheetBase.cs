using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ELM.Web.Pages.DailTimeSheet
{
    public class DailyTimeSheetBase :  ComponentBase
    {
        [Inject]
        public IDailyTimeSheetService dailyTimeSheetService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IEmployeeService employeeService { get; set; }
        public DailyTimeSheetDto DailyTimeSheetDto { get; set; } = new();
        public List<DailyTimeSheetDto> dailyTimeSheetDtos { get; set; } = new List<DailyTimeSheetDto>();
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string Search { get; set; } = string.Empty;
        public Pager Paging { get; set; } = new();
        public List<Employee> EmployeesList { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            EmployeesList = await employeeService.GetAllEmployee();
            dailyTimeSheetDtos = await dailyTimeSheetService.GetAllDailyTimeSheet();
        }

        public async Task<string> AddDailyTimeSheet()
        {
            var response = await dailyTimeSheetService.AddDailyTimeSheet(DailyTimeSheetDto);
            NavigationManager.NavigateTo("/DailyTask");
            return response;
        }
        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }

        public async Task GetAll(int currentPage = 1)
        {
            //var x = await ((ApiAuthenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
            //var user = x.User.IsInRole("User");
            Paging.CurrentPage = currentPage;
            Paging.StartDate = StartDate;
            Paging.EndDate = EndDate;
            Paging.Search = Search;
            var dailyTimeSheet = await dailyTimeSheetService.GetDailyTimeSheetWithFilter(Paging);
            dailyTimeSheetDtos = dailyTimeSheet.DataList;
            Paging = dailyTimeSheet.Pager;
            StateHasChanged();
        }
    }
}
