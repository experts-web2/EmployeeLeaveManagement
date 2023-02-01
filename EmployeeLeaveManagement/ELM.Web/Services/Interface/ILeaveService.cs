using DTOs;
using ELM.Helper.SupportFiles;
using ELM.Web.Helper;


namespace EmpLeave.Web.Services.Interface
{
    public interface ILeaveService
    {
        Task PostCall(LeaveDto leaveDto);
        Task<Response<LeaveDto>> GetAllLeaves(Pager Paging);
        Task UpdateCall(LeaveDto leaveDto);
        Task DeleteCall(int id);
        Task<LeaveDto> GetByIdCall(int id);
    }
}
