
using DAL.Interface.GenericInterface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace DAL.Interface
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        PagedList<EmployeeDto> GetAllEmployee(Pager pager);
        //void AddEmployee(EmployeeDto employee);
        void DeleteEmployee(int id);
        void Update(EmployeeDto employee);
        EmployeeDto GetById(int id);
        List<Employee> GetAllEmployees();
    }
}
