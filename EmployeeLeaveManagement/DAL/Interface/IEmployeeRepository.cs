
using DTOs;
using ELM.Helper;

namespace DAL.Interface
{
    public interface IEmployeeRepository
    {
        PagedList<EmployeeDto> GetAllEmployee(Pager pager);
        void AddEmployee(EmployeeDto employee);
        void DeleteEmployee(int id);
        void Update(EmployeeDto employee);
        EmployeeDto GetById(int id);

    }
}
