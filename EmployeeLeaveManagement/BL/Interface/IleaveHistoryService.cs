using DomainEntity.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IleaveHistoryService
    {
        List<LeaveHistoryDto> GetLeaveHistoryByEmployeeId(int EmployeeId);
    }
}
