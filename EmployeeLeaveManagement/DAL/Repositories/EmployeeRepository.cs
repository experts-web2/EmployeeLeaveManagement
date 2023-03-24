using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
   public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        //public void AddEmployee(EmployeeDto employee)
        //{
        //    Employee employeeEntity = ToEntity(employee);
        //    _dbContext.Employees.Add(employeeEntity);
        //    _dbContext.SaveChanges();

        //}
        private Employee ToEntity(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Id=employeeDto.ID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
                DateOfBrith = employeeDto.DateOfBrith,
                Gender = employeeDto.Gender,
                Email = employeeDto.Email,
                CurrentSalary=employeeDto.CurrentSalary

            };
            return employee;
        }
        public PagedList<EmployeeDto> GetAllEmployee(Pager pager)
        {
            var employees = _dbContext.Employees.Include(x => x.Leaves).AsQueryable();
            if (!string.IsNullOrEmpty(pager.Search))
            {
                employees = employees.
                    Where(x => x.FirstName.Contains(pager.Search.Trim()) ||
                           x.LastName.Contains(pager.Search.Trim()) ||
                           x.Address.Contains(pager.Search.Trim()) ||
                           x.Email.Contains(pager.Search.Trim()));
            }
            var paginatedList= PagedList<Employee>.ToPagedList(employees, pager.CurrentPage, pager.PageSize);
            var employeesDto = ToDtos(paginatedList);          
            return new PagedList<EmployeeDto>
                (employeesDto, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
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
                    employeeDto.FirstName= employee.FirstName;
                    employeeDto.LastName = employee.LastName;
                    employeeDto.Address = employee.Address;
                    employeeDto.DateOfBrith = employee.DateOfBrith;
                    employeeDto.Email = employee.Email;
                    employeeDto.Gender = employee.Gender;
                    employeeDto.CurrentSalary=employee.CurrentSalary;
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
        public void DeleteEmployee(int id)
        {
            try
            {
                var Deleted = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
                if (Deleted != null)
                {
                    _dbContext.Remove(Deleted);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(EmployeeDto employee)
        {
            if(employee != null)
            try
            {
                 var Updated = ToEntity(employee);
                _dbContext.Update(Updated);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public EmployeeDto GetById(int id)
        {
          var FindEmployee= _dbContext.Employees.Include(x=>x.Leaves).FirstOrDefault(x => x.Id == id);

            EmployeeDto employeeDto = SetEmployeeDto(FindEmployee);
            return employeeDto;
        }
        private static EmployeeDto SetEmployeeDto(Employee employee)
        {
            if (employee==null)
            {
                return null;
            }
            try
            {
                EmployeeDto employeeDto=new EmployeeDto()
                {
                    ID = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    DateOfBrith = employee.DateOfBrith,
                    Address = employee.Address, 
                    CurrentSalary=employee.CurrentSalary,
                    leaves = employee.Leaves.ToList()
                    
                };
                return employeeDto;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
        public List<Employee> GetAllEmployees()
        {
          return  _dbContext.Employees.ToList();
        }
    }
}
