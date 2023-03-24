using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository=employeeRepository;
        }
        public void AddEmployee(EmployeeDto employee)
        {
            Employee employeeEntity = ToEntity(employee);
            _employeeRepository.Add(employeeEntity);

        }
        private Employee ToEntity(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Id = employeeDto.ID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
                DateOfBrith = employeeDto.DateOfBrith,
                Gender = employeeDto.Gender,
                Email = employeeDto.Email,
                CurrentSalary = employeeDto.CurrentSalary

            };
            return employee;
        }
    }
}
