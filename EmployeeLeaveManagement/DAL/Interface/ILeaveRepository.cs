using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ILeaveRepository
    {
        List<LeaveDto> GetAllLeave();
        void Addleave(LeaveDto leaveDto);
        //Employee DeleteEmployee(int id);

        //void Update(EmployeeDto employee);
    }
}
