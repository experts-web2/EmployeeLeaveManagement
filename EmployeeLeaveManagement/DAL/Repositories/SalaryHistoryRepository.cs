using DAL.Interface;
using DomainEntity.Models;
using DTOs;
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
        public List<SalaryHistoryDto> GetSalaries()
        {
            try
            {
                var salaries = dbContext.SalaryHistories.Include(x => x.Employee).ToList();
                List<SalaryHistoryDto> salariesDto = ToDto(salaries);
                return salariesDto;
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
