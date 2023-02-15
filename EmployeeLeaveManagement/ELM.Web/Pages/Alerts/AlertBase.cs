using DTOs;
using DomainEntity.Models;
using ELM.Web.Pages.EmployeePage;
using ELM.Web.Services.ServiceRepo;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.Alerts
{
    public class AlertBase : ComponentBase
    {
        [Inject]
        public IAlertService AlertService { get; set; }
        public List<DomainEntity.Models.Alert> Alerts { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
          Alerts = await AlertService.GetAlerts();
          
        }
    }
}
