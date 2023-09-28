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

        public List<LoanDto> loanDtos = new List<LoanDto>();

        protected override async Task OnInitializedAsync()
        {
            await GetLoans();
        }
        public async Task GetLoans()
        {
           loanDtos = await loanService.GetLoans();
        }

    }
}
