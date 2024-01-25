using DTOs;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface ILoanInstallmentHistoryService
    {
        Task<List<LoanInstallmentHistoryDto>> GetEmployeeLoanInstallmentHistory();
    }
}
