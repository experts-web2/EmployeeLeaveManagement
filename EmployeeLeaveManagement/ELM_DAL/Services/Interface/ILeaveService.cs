using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace EmpLeave.Web.Services.Interface
{
    public interface ILeaveService
    {
        Task AddLeave(LeaveDto leaveDto);
        Task<Response<LeaveDto>> GetAllLeaves(Pager paging);
        Task EditLeave(LeaveDto leaveDto);
        Task DeleteLeave(int id);
        Task<LeaveDto> GetByIdCall(int id);
        Task<List<Leave>> GetEmployeeLeaves();
    }
}
