
using ELM_DAL.Services.Interface;
using ELM.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using ELM.Helper;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using BL.Service;

namespace ELM.Web.Pages.Alerts
{
    public class AlertBase : ComponentBase
    {
        [Inject]
        public IAttendenceService AttendenceService { get; set; }
        [Inject]
        public IAlertService AlertService { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        public Pager Pager { get; set; } = new();
        public List<DomainEntity.Models.Alert> Alerts { get; set; } = new();
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;
        public DateTime? EndDate { get; set; } = DateTime.Now.Date;
        public string search { get; set; } = string.Empty;
        public Pager Paging { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            //var user = await authenticationStateTask;
            //var u = user.User;
            //string employeeId = u.FindFirstValue(ClaimTypes.NameIdentifier);
            //var attendences = await AttendenceService.GetAttendencesWithoutPagination(employeeId,u);
            //List<DateTime> attendenceDates = attendences.Where(x => x.Timeout != null).Select(y => y.AttendenceDate.Date).ToList();
            //if(!string.IsNullOrEmpty(employeeId))
            // await  AlertService.DeleteAlert(int.Parse(employeeId), attendenceDates);

            await GetAll();
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
