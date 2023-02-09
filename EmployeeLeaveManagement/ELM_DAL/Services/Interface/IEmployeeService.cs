using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace EmpLeave.Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeDto employeeDto);
        Task<Response<EmployeeDto>> GetAllEmployee(Pager paging);
        Task UpdateEmployee(EmployeeDto employeeDto);
        Task DeleteEmployeebyId(int id);
        Task<EmployeeDto> GetEmployeebyId(int id);
         Task<List<Employee>> GetAllEmployee();

    }
}
