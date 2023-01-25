using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface ILeaveService
    {
        Task PostCall(LeaveDto leaveDto);
        Task<List<LeaveDto>> GetAllLeaves();
        Task UpdateCall(LeaveDto leaveDto);
        Task DeleteCall(int id);
        Task<LeaveDto> GetByIdCall(int id);
    }
}
