using DTOs;

namespace ELM.Web.Services.Interface
{
    public interface IAttendenceService
    {
        Task PostCall(AttendenceDto attendenceDto);
        Task<List<AttendenceDto>> GetAttendences();
        Task DeleteCall(int id);

    }
}
