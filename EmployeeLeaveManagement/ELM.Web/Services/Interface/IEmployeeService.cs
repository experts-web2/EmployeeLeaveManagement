using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeDto employeeDto);
        Task<List<EmployeeDto>> GetAllEmployee();
        Task EditEmployee(EmployeeDto employeeDto);
        Task DeleteEmployee(int id);
        Task<EmployeeDto> GetByIdCall(int id);

    }
}
