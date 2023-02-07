using DAL.Interface;
using DomainEntity.Models;
using DTOs;
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
          private SalaryHistory ToEntity(SalaryHistoryDto salaryDto)
          {
            SalaryHistory salary = new()
            {
                NewSalary=salaryDto.NewSalary,
                IncrementDate=salaryDto.IncrementDate,
                EmployeeId=salaryDto.EmployeeId
            };
            return salary;
          }
    }
}
