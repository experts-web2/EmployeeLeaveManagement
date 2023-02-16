using DTOs;
using DomainEntity.Models;
using ELM.Web.Pages.EmployeePage;
using ELM.Web.Services.ServiceRepo;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
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

        protected override async Task OnInitializedAsync()
        {
            await GetAll();
          
        }
        public async Task GetAll(int currentPage = 1)
        {
            Pager.CurrentPage = currentPage;
           Response<DomainEntity.Models.Alert> AlertList = await AlertService.GetAlerts(Pager);
            Alerts = AlertList.DataList;
            Pager = AlertList.Pager;
            StateHasChanged();
        }
    }
}
