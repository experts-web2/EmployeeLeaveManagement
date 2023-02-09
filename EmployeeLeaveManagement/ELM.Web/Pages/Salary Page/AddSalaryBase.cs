using DomainEntity.Models;
using DTOs;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.Salary_Page
{
    public class AddSalaryBase:ComponentBase
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
         protected override async Task OnInitializedAsync()
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
                await SalaryHistoryService.AddSalary(SalaryHistoryDto);
            }

            Cancel();
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/ListOfSalaries");
        }
    }
}
