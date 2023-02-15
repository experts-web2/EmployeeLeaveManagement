using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalaryHistoryRepository : ISalaryHistoryRepository
    {
        private AppDbContext dbContext;
        public SalaryHistoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddSalary(SalaryHistoryDto salaryDto)
        {
            try
            {
                SalaryHistory salary = ToEntity(salaryDto);
                dbContext.Add(salary);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PagedList<SalaryHistoryDto> GetSalaries(Pager pager)
        {
            try
            {
                var salaries = dbContext.SalaryHistories.Include(x => x.Employee).AsQueryable();
                if (pager.StartingDate is not null && pager.CurrentDate != DateTime.MinValue && pager.EmployeeId > 0)
                {
                    salaries = salaries.Where(x => x.IncrementDate >= pager.StartingDate &&
                    x.IncrementDate <= pager.CurrentDate &&
                    x.EmployeeId == pager.EmployeeId);
                }
                else if (pager.EmployeeId > 0)
                {
                    salaries = salaries.Where(x => x.EmployeeId == pager.EmployeeId);
                }
                else if (pager.StartingDate is not null && pager.CurrentDate != DateTime.MinValue)
                {
                    salaries = salaries.Where(x => x.IncrementDate >= pager.StartingDate && x.IncrementDate <= pager.CurrentDate);
                }
                else
                    salaries = salaries.Where(x => x.IncrementDate <= pager.CurrentDate);
                var paginatedList = PagedList<SalaryHistory>.ToPagedList(salaries, pager.CurrentPage, pager.PageSize);
                var SalariesDto = ToDto(paginatedList);
                return new PagedList<SalaryHistoryDto>
                    (SalariesDto, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SalaryHistoryDto GetSalary(int id)
        {
            try
            {
                var salary = dbContext.SalaryHistories.Include(s => s.Employee).FirstOrDefault(x => x.Id == id);
                SalaryHistoryDto salaryDto = SetSalaryToDto(salary);
                return salaryDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void EditSalary(SalaryHistoryDto salaryDto)
        {
            try
            {
                SalaryHistory salary = ToEntity(salaryDto);
                dbContext.Update(salary);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteSalary(int id)
        {
            try
            {
                SalaryHistory salary = dbContext.SalaryHistories.Where(x => x.Id == id).FirstOrDefault();
                if (salary != null)
                {
                    dbContext.Remove(salary);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private SalaryHistory ToEntity(SalaryHistoryDto salaryDto)
        {
            SalaryHistory salary = new()
            {
                Id=salaryDto.ID,
                NewSalary = salaryDto.NewSalary,
                IncrementDate = salaryDto.IncrementDate,
                EmployeeId = salaryDto.EmployeeId
            };
            return salary;
        }
        private List<SalaryHistoryDto> ToDto(List<SalaryHistory> salaries)
        {
            List<SalaryHistoryDto> salariesDto = new();
            foreach (var salary in salaries)
            {
                SalaryHistoryDto salaryDto = new()
                {
                    ID=salary.Id,
                    NewSalary = salary.NewSalary,
                    IncrementDate = salary.IncrementDate,
                    FirstName = salary.Employee.FirstName,
                    LastName = salary.Employee.LastName
                };
                salariesDto.Add(salaryDto);
            }
            return salariesDto;
        }
        private SalaryHistoryDto SetSalaryToDto(SalaryHistory salary)
        {
            SalaryHistoryDto salaryDto = new()
            {
                ID=salary.Id,
                NewSalary = salary.NewSalary,
                IncrementDate = salary.IncrementDate,
                FirstName = salary.Employee.FirstName,
                LastName = salary.Employee.LastName,
                EmployeeId=salary.EmployeeId
            };
            return salaryDto;
        }
    }
}
