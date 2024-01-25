using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IleaveHistoryService
    {
        List<LeaveHistory> GetLeaveHistoryByEmployeeId(int EmployeeId);
    }
}
