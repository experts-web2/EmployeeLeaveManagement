using DTOs;
using ELM.Web.Pages.EmployeePage;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using DomainEntity.Models;

namespace ELM.Web.Pages.SalaryPages
{
    public class ListOfSalariesBase : ComponentBase
    {
        [Inject]
        private ISalaryService _salaryService { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public List<SalaryDto> salaryDtos = new();
        public List<Employee> employeeList = new();

        public int EmployeeId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            employeeList = await EmployeeService.GetAllEmployee();
            await GetAllSalaries();
        }

        public async Task PayLoan()
        {
            int id = EmployeeId;
            await _salaryService.AddSalary();
        }

        public async Task<List<SalaryDto>> GetAllSalaries()
        {
            salaryDtos = await _salaryService.GetSalaries();
            if (salaryDtos != null)
            {
                return salaryDtos;
            }
            else
                return salaryDtos;
        }



    }
}
