using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class LoanInstallmentHistoryService : ILoanInstallmentHistoryService
    {
        private readonly ILoanInstallmentHistoryRepository _LoanInstallmentHistoryRepository;

        public LoanInstallmentHistoryService(ILoanInstallmentHistoryRepository loanInstallmentHistoryRepository)
        {
            _LoanInstallmentHistoryRepository = loanInstallmentHistoryRepository;
        }

        public List<LoanInstallmentHistoryDto> GetAllLoanHistory()
        {
            var loanInstallmentHistory = _LoanInstallmentHistoryRepository.GetAll();
           return loanInstallmentHistory.Select(setLoanInstallmentDto).ToList();
        }

        public List<LoanInstallmentHistoryDto> GetAllLoanHistoryofEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        private LoanInstallmentHistoryDto setLoanInstallmentDto(LoanInstallmentHistory loanInstallmentHistory)
        {
            return new LoanInstallmentHistoryDto() 
            { 
                InstallmentAmount = loanInstallmentHistory.InstallmentAmount,
                LoanId = loanInstallmentHistory.LoanId
            };

        }
    }
}
