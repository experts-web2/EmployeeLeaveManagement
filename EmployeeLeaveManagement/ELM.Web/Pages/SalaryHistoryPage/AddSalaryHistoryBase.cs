using DomainEntity.Models;
using DTOs;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.SalaryHistoryPage
{
    public class AddSalaryHistoryBase:ComponentBase
    {
        [Inject]
        public ISalaryHistory SalaryHistoryService { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? ID { get; set; }
        public List<Employee> EmployeesList { get; set; } = new();
        public SalaryHistoryDto SalaryHistoryDto { get; set; } = new();

         protected override async Task OnParametersSetAsync()
        {
            EmployeesList = await  EmployeeService.GetAllEmployee();
            if (ID.HasValue)
            {
                SalaryHistoryDto =await SalaryHistoryService.GetSalaryById(ID.Value);
            }
        }
        protected async Task SaveSalary()
        {

            if (!ID.HasValue)
            {
                if(SalaryHistoryDto.NewSalary>0)
                await SalaryHistoryService.AddSalary(SalaryHistoryDto);
            }
            else
            {
                if(SalaryHistoryDto.NewSalary>0)
                await SalaryHistoryService.UpdateSalary(SalaryHistoryDto);
            }
            Cancel();
        }
        protected void Cancel()
        {
            NavigationManager.NavigateTo("ListOfSalaryHistory");
        }
    }
}
