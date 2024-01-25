using BL.Interface;
using DAL.Interface;
using DAL.Interface.GenericInterface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
       
        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public void AddLoan(LoanDto loanDto)
        {
            var loan = setLoanEntity(loanDto);
            _loanRepository.Add(loan);
        }

        public void DeleteSalary(int id)
        {
            _loanRepository.deletebyid(id);
        }


        public List<LoanDto> GetAllLoans()
        {

           var listOfLoans = _loanRepository.GetAll().Include(x=>x.Employee);
           return listOfLoans.Select(setLoanDtoEntity).ToList();
        }

        public LoanDto GetById(int Id)
        {
            var Loan =_loanRepository.GetByID(Id);
            return setLoanDtoEntity(Loan);
        }

        public List<LoanDto> GetLoanByEmployeeId(int EmployeeId)
        {
            var Loan = _loanRepository.Get(x=>x.EmployeeId == EmployeeId, y=>y.Employee);
            return Loan.Select(setLoanDtoEntity).ToList();
        }

        public LoanDto Update(LoanDto loanDto)
        {
           var addedLoanOfEmployee = _loanRepository.GetLoanWithEmployeeId(loanDto.EmployeeId);
            if (addedLoanOfEmployee != null)
            {
                addedLoanOfEmployee.LoanDate = loanDto.LoanDate;
                addedLoanOfEmployee.LoanAmount = addedLoanOfEmployee.RemainingAmount + loanDto.LoanAmount;
                addedLoanOfEmployee.LoanStatus = loanDto.LoanStatus;
                addedLoanOfEmployee.LoanType = loanDto.LoanType;
                addedLoanOfEmployee.Active = loanDto.Active;
                addedLoanOfEmployee.EmployeeId = loanDto.EmployeeId;
                addedLoanOfEmployee.InstallmentPlan = loanDto.InstallmentPlan;
                addedLoanOfEmployee.InstallmentAmount = addedLoanOfEmployee.LoanAmount / (decimal)loanDto.InstallmentPlan;
                addedLoanOfEmployee.RemainingAmount = addedLoanOfEmployee.LoanAmount;
                _loanRepository.update(addedLoanOfEmployee);
            }
            else
                return new LoanDto();
            return loanDto;
        }

        private Loan setLoanEntity(LoanDto loanDto)
        {
            var laon = new Loan()
            {
                Id = loanDto.ID,
                LoanAmount = loanDto.LoanAmount,
                LoanDate = loanDto.LoanDate,
                LoanType = loanDto.LoanType,
                LoanStatus = loanDto.LoanStatus,
                Active = loanDto.Active,
                InstallmentPlan = loanDto.InstallmentPlan,
                EmployeeId = loanDto.EmployeeId,
                InstallmentAmount = loanDto.LoanAmount / (decimal)loanDto.InstallmentPlan!.Value,
                RemainingAmount = loanDto.LoanAmount

            };
            return laon;
        }

        private LoanDto setLoanDtoEntity(Loan loan)
        {
            var laonDto = new LoanDto()
            {
                ID = loan.Id,
                LoanAmount = loan.LoanAmount,
                LoanDate = loan.LoanDate,
                LoanType = loan.LoanType,
                LoanStatus = loan.LoanStatus,
                Active = loan.Active,
                InstallmentPlan = loan.InstallmentPlan,
                EmployeeId = loan.EmployeeId,
                InstallmentAmount = loan.InstallmentAmount,
                FirstName = loan.Employee.FirstName,
                RemainingAmount = loan.RemainingAmount

            };
            return laonDto;
        }
        public PagedList<LoanDto> GetAllLoans(Pager pager)
        {
            throw new NotImplementedException();
        }

    }
}
