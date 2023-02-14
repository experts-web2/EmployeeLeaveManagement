using DomainEntity.Models;
using DTOs;

namespace DAL.Interface
{
    public interface IJobService
    {
        List<Employee> GetAbsentEmployee();
        List<AttendenceDto> GetAllAttendences();
    }
}
