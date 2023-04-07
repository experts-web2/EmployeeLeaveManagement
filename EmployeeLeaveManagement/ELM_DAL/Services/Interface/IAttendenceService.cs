using DTOs;
using ELM.Helper;

namespace ELM.Web.Services.Interface
{
    public interface IAttendenceService
    {
        Task AddAttendence(AttendenceDto attendenceDto);
        Task<Response<AttendenceDto>> GetAttendences(Pager paging);
        Task DeleteAttendence(int id);
        Task UpdateAttendence(AttendenceDto attendenceDto);
        Task<AttendenceDto> GetByID(int value);
        Task<AttendenceDto> GetAttendenceByEmployeeId(int employeeId);
    }
}
