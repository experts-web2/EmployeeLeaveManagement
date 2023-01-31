using DTOs;
using ELM.Web.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Services.Interface
{
    public interface ILeaveService
    {
        Task PostCall(LeaveDto leaveDto);
        Task<Response<LeaveDto>> GetAllLeaves(Parameter parameter);
        Task UpdateCall(LeaveDto leaveDto);
        Task DeleteCall(int id);
        Task<LeaveDto> GetByIdCall(int id);
    }
}
