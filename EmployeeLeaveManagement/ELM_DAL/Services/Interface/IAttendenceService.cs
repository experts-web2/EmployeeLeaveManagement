using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System.Security.Claims;

namespace ELM.Web.Services.Interface
{
    public interface IAttendenceService
    {
        Task AddAttendence(AttendenceDto attendenceDto);
        Task<Response<AttendenceDto>> GetAttendences(Pager paging);
        Task<List<AttendenceDto>> GetAttendencesWithoutPagination(string employeeId,ClaimsPrincipal user);
        Task DeleteAttendence(int id);
        Task UpdateAttendence(AttendenceDto attendenceDto);
        Task<AttendenceDto> GetByID(int value);
        Task<AttendenceDto> GetAttendenceByEmployeeId(int employeeId);
       Task<AttendenceDto> GetAttendenceByAlertDateAndEmployeeId(DateTime alertDate, int employeeId);
    }
}
