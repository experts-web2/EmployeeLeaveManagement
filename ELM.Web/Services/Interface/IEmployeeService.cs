using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task PostCall(EmployeeDto employeeDto);
        Task<List<EmployeeDto>> GetAllEmployee();
        Task UpdateCall(EmployeeDto employeeDto);
        Task DeleteCall(int id);
        Task<EmployeeDto> GetByIdCall(int id);

    }
}
