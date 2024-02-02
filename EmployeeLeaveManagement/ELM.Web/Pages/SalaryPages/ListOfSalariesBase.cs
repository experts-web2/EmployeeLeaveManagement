using DTOs;
using ELM.Web.Pages.EmployeePage;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using DomainEntity.Models;
using Microsoft.JSInterop;

namespace ELM.Web.Pages.SalaryPages
{
    public class ListOfSalariesBase : ComponentBase
    {
        [Inject]
        private ISalaryService _salaryService { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public List<SalaryDto> salaryDtos = new();
        public List<Employee> employeeList = new();
        public decimal TotalAmount { get; set; }
        public int EmployeeId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            employeeList = await EmployeeService.GetAllEmployee();
            await GetAllSalaries();
        }

        public async Task<string> UpdateEmployeeSalary(List<SalaryDto> salaryDtos)
        {
            return await _salaryService.UpdateEmployeeSalaryAsync(salaryDtos);
        }
        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }

        public async Task PayLoan()
        {
            await _salaryService.PayLoanOfEmployee(EmployeeId);
        }

        public async Task<List<SalaryDto>> GetAllSalaries()
        {
            salaryDtos = await _salaryService.GetSalaries();
            if (salaryDtos != null)
            {
                TotalAmount = salaryDtos.Sum(x => x.TotalSalary);
                return salaryDtos;
            }
            else
                return salaryDtos;
            
        }



    }
}
