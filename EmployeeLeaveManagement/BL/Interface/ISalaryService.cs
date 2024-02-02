using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ISalaryService
    {
        void AddSalaryorUpdate(int Id);
        PagedList<SalaryDto> GetAllSalaries(Pager pager);
        void DeleteSalary(int id);
        SalaryDto GetById(int id);
        void Update(SalaryDto Salary);
        List<SalaryDto> GetAllSalary();
        List<SalaryDto> GetAllSalariesByEmployeeId(int employeeId);
        string UpdateEmployeeSalary(List<SalaryDto> salaryDto);
    }
}

