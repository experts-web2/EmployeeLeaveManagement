using DTOs;

namespace DAL.Interface
{
    public interface IEmployeeRepository
    {
        List<EmployeeDto> GetAllEmployee();
        void AddEmployee(EmployeeDto employee);
        void DeleteEmployee(int id);

        void Update(EmployeeDto employee);
        EmployeeDto GetById(int id);

    }
}
