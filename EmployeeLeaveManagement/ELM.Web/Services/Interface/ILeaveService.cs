using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface ILeaveService
    {
        Task AddLeave(LeaveDto leaveDto);
        Task<List<LeaveDto>> GetAllLeaves();
        Task EditLeave(LeaveDto leaveDto);
        Task DeleteLeave(int id);
        Task<LeaveDto> GetByIdCall(int id);
    }
}
