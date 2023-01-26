using DTOs;
using ELM.Web.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task PostCall(EmployeeDto employeeDto);
        Task<Response<EmployeeDto>> GetAllEmployee(Parameter parameter);
        Task UpdateCall(EmployeeDto employeeDto);
        Task DeleteCall(int id);
        Task<EmployeeDto> GetByIdCall(int id);

    }
}
