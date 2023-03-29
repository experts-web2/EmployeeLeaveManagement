
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;
using ELM.Helper;

namespace ELM.Web.Pages.Alerts
{
    public class AlertBase : ComponentBase
    {
        [Inject]
        public IAlertService AlertService { get; set; }
        public Pager Pager { get; set; } = new();
        public List<DomainEntity.Models.Alert> Alerts { get; set; } = new();
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;
        public DateTime? EndDate { get; set; } = DateTime.Now.Date;
        public string search { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync() => await GetAll();
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
