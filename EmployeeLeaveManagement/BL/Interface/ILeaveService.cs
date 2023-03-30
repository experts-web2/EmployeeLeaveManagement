using DTOs;
using ELM.Helper;

namespace BL.Interface
{
    public interface ILeaveService
    {
        PagedList<LeaveDto> GetAll(Pager pager);
        LeaveDto GetById(int id);
        LeaveDto Add(LeaveDto leave);
        void Delete(int id);
        LeaveDto Update(LeaveDto leave);
        Task<List<LeaveDto>> GetLeavesByEmployeeID(int employeeId);
    }
}
