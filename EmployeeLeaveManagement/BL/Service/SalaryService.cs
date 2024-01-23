﻿using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class SalaryService : ISalaryService
    {
        private ISalaryRepository _salaryRepository;
        private ISalaryHistoryService _salaryHistoryService;
        private ILoanRepository _loanRepository;
        private ILoanInstallmentHistoryRepository _loanInstallmentHistoryRepository;

        public SalaryService(ISalaryRepository salaryRepository, ISalaryHistoryService salaryHistoryService, ILoanRepository loanRepository, ILoanInstallmentHistoryRepository loanInstallmentHistoryRepository)
        {
            _salaryRepository = salaryRepository;
            _salaryHistoryService = salaryHistoryService;
            _loanRepository = loanRepository;
            _loanInstallmentHistoryRepository = loanInstallmentHistoryRepository;
        }
        public void AddSalaryorUpdate(int employeeId)
        {
            var salaryOFEmployee = _salaryRepository.GetByExpression(x=>x.EmployeeId == employeeId, x=>x.Employee);
            if (salaryOFEmployee!= null)
            {
                setSalaryEntity(salaryOFEmployee);
            
                _salaryRepository.update(salaryOFEmployee);
            }
            else
            {
                if (salaryOFEmployee == null)
                {
                    salaryOFEmployee =new Salary() { EmployeeId = employeeId };
                    setSalaryEntity(salaryOFEmployee);
                    _salaryRepository.Add(salaryOFEmployee);
                }

            }
        }

        public List<SalaryDto> GetAllSalary()
        {
           var allSalaries = _salaryRepository.GetAll().Include(x=>x.Employee);
            return allSalaries.Select(ToSalaryDto).ToList();                
        }

        private SalaryDto ToSalaryDto(Salary salary)
        {
            var salaryDto = new SalaryDto()
            {
                 LeaveDeduction = salary.LeaveDeduction,
                 LoanDeduction  = salary.LoanDeduction,
                 TotalDedection = salary.TotalDedection,
                 TotalSalary = salary.TotalSalary,
                 CurrentSalary = salary.CurrentSalary,
                 EmployeeName = salary.Employee.FirstName
            };
            return salaryDto;
        }
        public List<SalaryDto> GetAllSalariesByEmployeeId(int employeeId)
        {
           var listOfEmployeeSalary= _salaryRepository.GetAll().Include(y=>y.Employee).Where(x => x.EmployeeId == employeeId);
            return listOfEmployeeSalary.Select(ToSalaryDto).ToList();
        }
        public void setSalaryEntity(Salary? salary)
        {
           
            var salaryHistory = _salaryHistoryService.GetSalaryHistoryByEmployeeId(salary.EmployeeId)
                                                     .MaxBy(x => x.IncrementDate);
            var loan = _loanRepository.GetAll().Where(x=>x.EmployeeId == salary.EmployeeId).FirstOrDefault();

            LoanInstallmentHistory loanInstallmentHistory = new();
            int allowedLeaves = 20;

            if (salaryHistory != null && salaryHistory!.Employee.Leaves.Count > allowedLeaves)
            {
                salary.LeaveDeduction = calculateLeaveWithSalaryDedection(salaryHistory, allowedLeaves);  
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary;
                salary.LoanDeduction = loan.InstallmentAmount;
                salary.Perks = 0;
                salary.TotalDedection = salary.LeaveDeduction + salary.LoanDeduction;
                salary.TotalSalary = salary.CurrentSalary - salary.TotalDedection + salary.Perks;
                loan.RemainingAmount = loan.RemainingAmount - loan.InstallmentAmount;
                _loanRepository.update(loan);
                loanInstallmentHistory.InstallmentAmount = loan.InstallmentAmount;
                loanInstallmentHistory.LoanId = loan.Id;
                _loanInstallmentHistoryRepository.Add(loanInstallmentHistory);

            }

            else if(loan!= null && loan.RemainingAmount > 0 && loan.LoanAmount > 0)
            {
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary;
                salary.LeaveDeduction = 0;
                salary.Perks = 0;
                salary.GeneralDeduction = 0;
                salary.LoanDeduction = loan!.InstallmentAmount;
                salary.TotalDedection = salary.LoanDeduction + salary.GeneralDeduction;
                salary.TotalSalary = salary.CurrentSalary - salary.TotalDedection + salary.Perks;
                loan.RemainingAmount = loan.RemainingAmount - loan!.InstallmentAmount;
                _loanRepository.update(loan);
                loanInstallmentHistory.InstallmentAmount = loan.InstallmentAmount;
                loanInstallmentHistory.LoanId = loan.Id;
                _loanInstallmentHistoryRepository.Add(loanInstallmentHistory);

            }

            else 
            {
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary > 0 ? (decimal)salaryHistory.NewSalary : 0;
                salary.LeaveDeduction = 0;
                salary.LoanDeduction = 0;
                salary.TotalDedection = 0;
                salary.Perks = 0;
                salary.TotalSalary = salary.CurrentSalary - salary.TotalDedection + salary.Perks;
            }

        }

        private decimal calculateLeaveWithSalaryDedection(SalaryHistory salaryHistory, int allowedLeaves)
        {
            //var presentDays = salaryHistory.Employee
            //                                 .Attendences
            //.Count(x => x.AttendenceDate.Date.Month == DateTime.Now.Date.Month - 1);
          
           // var currentMonthWorkingDays = presentDays - absentDays;

            var absentDays = salaryHistory.Employee
                                         .Leaves
                                         .Count(x => x.CreatedDate!.Value.Month == DateTime.Now.Date.Month - 1);

            var oneDaySalary = salaryHistory.NewSalary / 22;
            var leaves = salaryHistory.Employee.Leaves.Count - allowedLeaves;
            var leaveDedection = leaves * (decimal)oneDaySalary;
            return leaveDedection;
        }

        public void DeleteSalary(int id)
        {
            throw new NotImplementedException();
        }

        public PagedList<SalaryDto> GetAllSalaries(Pager pager)
        {
            throw new NotImplementedException();
        }

       

        public SalaryDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(SalaryDto Salary)
        {
            throw new NotImplementedException();
        }

       
    }
}
    

