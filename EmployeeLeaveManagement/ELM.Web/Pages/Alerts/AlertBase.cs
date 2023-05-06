
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ELM.Web.Pages.Alerts
{
    public class AlertBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public List<Employee> EmployeeList { get; set; } = new();
        [Inject]
        public IAttendenceService AttendenceService { get; set; }
        [Inject]
        public IAlertService AlertService { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        public Pager Pager { get; set; } = new();
        public List<DomainEntity.Models.Alert> Alerts { get; set; } = new();
        public IReadOnlyDictionary<int, string> UserDropdown { get; set; } = new Dictionary<int, string>();
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;
        public DateTime? EndDate { get; set; } = DateTime.Now.Date;
        public string search { get; set; } = string.Empty;
        public Pager Paging { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await GetAll();
            EmployeeList = await EmployeeService.GetAllEmployee();
        }

        public void OnOptionSelected()
        {
            if (search == "")
            {
                Response<DomainEntity.Models.Alert> AlertList =  AlertService.GetAlerts(Pager).GetAwaiter().GetResult();
                Alerts = AlertList.DataList;
            }
            else
            {

            }
        }

        public async Task GetAll(int currentPage = 1)
        {
            Pager.CurrentPage = currentPage;
            Pager.StartDate = StartDate;
            Pager.EndDate = EndDate;
            Pager.Search = search;
            Response<DomainEntity.Models.Alert> AlertList = await AlertService.GetAlerts(Pager);
            Alerts = AlertList.DataList;
            Pager = AlertList.Pager;
            StateHasChanged();

        }
    }
}
