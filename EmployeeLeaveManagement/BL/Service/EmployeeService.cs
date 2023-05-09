using BL.Interface;
using DAL.Interface;
using DAL.Repositories;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;

namespace BL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendenceRepository _attendenceRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IAttendenceRepository attendenceRepository)
        {
            _employeeRepository = employeeRepository;
            _attendenceRepository = attendenceRepository;
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

        public List<Employee> GetAbsentEmployees()
        {
            //Querry For getting Employees Whose are Absent
            return (from Employees in _employeeRepository.GetAll()
                    join Attendences in _attendenceRepository.Get(x => x.AttendenceDate.Date.Equals(DateTime.Now))
                    on Employees.Id equals Attendences.EmployeeId
                    into employeeAtendence
                    from attendence in employeeAtendence.DefaultIfEmpty()
                    where attendence == null || attendence.Timeout == null
                    select Employees).Include(x => x.Attendences).ToList();
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

        public void Update(EmployeeDto employeeDto)
        {
            try
            {
                var employee = _employeeRepository.GetByID(employeeDto.ID);
                ToEntity(employee,employeeDto);
                _employeeRepository.update(employee);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ToEntity(Employee employee, EmployeeDto employeeDto)
        {
            try
            {
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.Address = employeeDto.Address;
                employee.DateOfBrith = employeeDto.DateOfBrith;
                employee.Gender = employeeDto.Gender;
                employee.Email = employeeDto.Email;
                employee.CurrentSalary = employeeDto.CurrentSalary;
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

        private static EmployeeDto? SetEmployeeDto(Employee employee)
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
            catch
            {
                return null;
            }
        }
    }
}