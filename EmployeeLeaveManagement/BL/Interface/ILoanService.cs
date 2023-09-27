using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ILoanService
    {
        void AddLoan(LoanDto loanDto);
        PagedList<LoanDto> GetAllLoans(Pager pager);
        void DeleteSalary(int id);
        LoanDto GetById(int id);
        LoanDto Update(LoanDto loanDto);
        List<LoanDto> GetAllLoans();
    }
}
