using DomainEntity.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ILoanInstallmentHistoryService
    {
        List<LoanInstallmentHistoryDto> GetAllLoanHistory();
        List<LoanInstallmentHistoryDto> GetAllLoanHistoryofEmployee(int employeeId);

    }
}
