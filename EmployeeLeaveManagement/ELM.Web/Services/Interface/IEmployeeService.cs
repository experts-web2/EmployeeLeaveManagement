using DTOs;
using ELM.Helper.SupportFiles;
using ELM.Web.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface IEmployeeService
    {
        Task PostCall(EmployeeDto employeeDto);
        Task<Response<EmployeeDto>> GetAllEmployee(Pager paging);
        Task UpdateEmployee(EmployeeDto employeeDto);
        Task DeleteEmployeebyId(int id);
        Task<EmployeeDto> GetEmployeebyId(int id);

    }
}
