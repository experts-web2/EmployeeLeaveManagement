using DTOs;
using ELM.Web.Pages.EmployeePage;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace ELM.Web.Pages.Loan_Pages
{
    public class ListOfLoansBase : ComponentBase
    {
        [Inject]
        private ILoanService loanService { get; set; }
        [Inject]
        private ILoanInstallmentHistoryService loanInstallmentHistoryService { get; set; }

        public List<LoanDto> loanDtos = new List<LoanDto>();
        public List<LoanInstallmentHistoryDto> loanInstallmentHistoryDtos = new List<LoanInstallmentHistoryDto>();

        protected override async Task OnInitializedAsync()
        {
            await GetLoans();
            loanInstallmentHistoryDtos = await loanInstallmentHistoryService.GetEmployeeLoanInstallmentHistory();
        }
        public async Task GetLoans()
        {
           var response = await loanService.GetLoans();
            if (response != null)
            {
                loanDtos = response;
            }
            else
                loanDtos = new();
        }

    }
}
