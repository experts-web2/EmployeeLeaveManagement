using BL.Interface;
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

namespace BL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void AddEmployee(EmployeeDto employee)
        {
            Employee employeeEntity = ToEntity(employee);
            _employeeRepository.Add(employeeEntity);
        }
        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _employeeRepository.GetAll().ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public PagedList<EmployeeDto> GetAllEmployee(Pager pager)
        {
            try
            {
                IQueryable<Employee> employees = _employeeRepository.GetAll().Include(x => x.Leaves);
                if (!string.IsNullOrEmpty(pager.Search))
                {
                    employees = employees.
                        Where(x => x.FirstName.Contains(pager.Search.Trim()) ||
                               x.LastName.Contains(pager.Search.Trim()) ||
                               x.Address.Contains(pager.Search.Trim()) ||
                               x.Email.Contains(pager.Search.Trim()));
                }
                var paginatedList = PagedList<Employee>.ToPagedList(employees, pager.CurrentPage, pager.PageSize);
                var employeesDto = ToDtos(paginatedList);
                return new PagedList<EmployeeDto>
                    (employeesDto, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteEmployee(int id)
        {
            try
            {
                _employeeRepository.deletebyid(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public EmployeeDto GetById(int id)
        {
            try
            {
                Employee? employee = _employeeRepository.Get(x => x.Id == id, x => x.Leaves)?.FirstOrDefault();
                if (employee is null) return new EmployeeDto();
                EmployeeDto employeeDto = SetEmployeeDto(employee);
                return employeeDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(EmployeeDto employee)
        {
            try
            {
                var updateEmployee = ToEntity(employee);
                _employeeRepository.update(updateEmployee);
            }
            catch (Exception)
            {

                throw;
            }
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
        private List<EmployeeDto> ToDtos(List<Employee> employees)
        {
            try
            {
                List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
                foreach (var employee in employees)
                {
                    EmployeeDto employeeDto = new EmployeeDto();
                    employeeDto.ID = employee.Id;
                    employeeDto.FirstName = employee.FirstName;
                    employeeDto.LastName = employee.LastName;
                    employeeDto.Address = employee.Address;
                    employeeDto.DateOfBrith = employee.DateOfBrith;
                    employeeDto.Email = employee.Email;
                    employeeDto.Gender = employee.Gender;
                    employeeDto.CurrentSalary = employee.CurrentSalary;
                    employeeDto.leaves = employee.Leaves.ToList();

                    employeeDtos.Add(employeeDto);
                }
                return employeeDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static EmployeeDto SetEmployeeDto(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }
            try
            {
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    ID = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    DateOfBrith = employee.DateOfBrith,
                    Address = employee.Address,
                    CurrentSalary = employee.CurrentSalary,
                    leaves = employee.Leaves.ToList()

                };
                return employeeDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
