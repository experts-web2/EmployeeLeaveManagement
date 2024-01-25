using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class LeaveHistoryService : IleaveHistoryService
    {
        private ILeaveHistoryRepository _leaveHistoryRepository { get; set; }
        public LeaveHistoryService(ILeaveHistoryRepository leaveHistoryRepository)
        {
            _leaveHistoryRepository = leaveHistoryRepository;
        }
        public List<LeaveHistory> GetLeaveHistoryByEmployeeId(int EmployeeId)
        {
           return _leaveHistoryRepository.Get(x=>x.EmployeeId == EmployeeId).ToList();
        }
    }
}
