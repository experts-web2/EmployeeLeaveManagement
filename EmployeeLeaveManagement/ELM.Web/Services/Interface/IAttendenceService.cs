using DTOs;

namespace ELM.Web.Services.Interface
{
    public interface IAttendenceService
    {
        Task PostCall(AttendenceDto attendenceDto);
        Task<List<AttendenceDto>> GetAttendences();
        Task UpdateCall(LeaveDto leaveDto);
        Task DeleteCall(int id);
        Task<LeaveDto> GetByIdCall(int id);
    }
}
