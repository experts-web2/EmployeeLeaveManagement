using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface ILoanService 
    {
        public Task AddLoan(LoanDto loanDto);
        Task<LoanDto> GetLoanById(int id);
        Task<List<LoanDto>> GetLoans();
        Task DeleteLoan(int id);
        Task UpdateLoan(LoanDto loanDto);
        Task<LoanDto> GetLoanByEmployeeId(int employeeId);
    }
}
