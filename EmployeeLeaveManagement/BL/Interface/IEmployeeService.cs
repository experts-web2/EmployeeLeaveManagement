using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeDto employee);
        PagedList<EmployeeDto> GetAllEmployee(Pager pager);
        void DeleteEmployee(int id);
        EmployeeDto GetById(int id);
        void Update(EmployeeDto employee);
        List<Employee> GetAllEmployees();
    }
}
