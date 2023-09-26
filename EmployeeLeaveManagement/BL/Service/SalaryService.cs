using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Mvc;
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

        public SalaryService(ISalaryRepository salaryRepository, ISalaryHistoryService salaryHistoryService)
        {
            _salaryRepository = salaryRepository;
            _salaryHistoryService = salaryHistoryService;
        }
        public void AddSalary(int employeeId)
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
           var allSalaries = _salaryRepository.GetAll();
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
                 CurrentSalary = salary.CurrentSalary
            };
            return salaryDto;
        }

        public void setSalaryEntity(Salary? salary)
        {
           
            var salaryHistory = _salaryHistoryService.GetSalaryHistoryByEmployeeId(salary.EmployeeId)
                                                     .MaxBy(x => x.IncrementDate);
            int allowedLeaves = 20, loadDeduction= 500;
            if (salaryHistory!.Employee.Leaves.Count > allowedLeaves)
            {
                salary.LeaveDeduction = calculateLeaveWithSalaryDedection(salaryHistory, allowedLeaves);  
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary;
                salary.LoanDeduction = loadDeduction;
                salary.TotalDedection = salary.LeaveDeduction + salary.LoanDeduction;
                salary.Perks = 0;
                salary.TotalSalary = salary.CurrentSalary - salary.TotalDedection + salary.Perks;

            }
            else if(salary.LoanDeduction > 0)
            {
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary;
                salary.LeaveDeduction = 0;
                salary.Perks = 0;
                salary.TotalDedection = loadDeduction;
                salary.LoanDeduction = loadDeduction;
                salary.TotalSalary = salary.CurrentSalary - salary.TotalDedection + salary.Perks;

            }
            else 
            {
                salary.CurrentSalary = (decimal)salaryHistory.NewSalary;
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
          
            var absentDays = salaryHistory.Employee
                                         .Leaves
                                         .Count(x => x.CreatedDate!.Value.Month == DateTime.Now.Date.Month - 1);

           // var currentMonthWorkingDays = presentDays - absentDays;
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
    

