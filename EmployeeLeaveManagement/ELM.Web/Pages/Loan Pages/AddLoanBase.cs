using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace ELM.Web.Pages.Loan_Pages
{
    public class AddLoanBase : ComponentBase
    {
        [Inject]
        private ILoanService _loanService { get; set; }
        public LoanDto loanDto { get; set; } = new();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public List<Employee> EmployeeList { get; set; } = new();
        public Pager Paging { get; set; } = new();
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Parameter]
        public int? ID { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await GetEmployees();
            if (ID.HasValue)
            {

                loanDto = await _loanService.GetLoanById(ID.Value);
            }
        }
        protected async Task SaveLoan()
        {
            var Loan = await _loanService.GetLoanByEmployeeId(loanDto.EmployeeId);
            if (Loan != null)
            {
                loanDto.ID = Loan.ID;
                if (loanDto.LoanAmount > 0)
                    await _loanService.AddLoan(loanDto);
            }
            else if (loanDto.ID == 0)
            {
                await _loanService.AddLoan(loanDto);
                NavigationManager.NavigateTo("/ListOfLoans");
            }

            Cancel();
        }

        public async Task GetEmployees()
        {
            EmployeeList = await EmployeeService.GetAllEmployee();
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
