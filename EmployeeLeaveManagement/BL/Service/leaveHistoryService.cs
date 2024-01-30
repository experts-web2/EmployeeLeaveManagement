using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
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
        public List<LeaveHistoryDto> GetLeaveHistoryByEmployeeId(int EmployeeId)
        {
           var response = _leaveHistoryRepository.Get(x=>x.EmployeeId == EmployeeId, y=>y.Leave.Employee).OrderByDescending(x=>x.CreatedDate).ToList();
            return response.Select(ToSetLeaveDto).ToList();
        }

        public LeaveHistoryDto ToSetLeaveDto(LeaveHistory leaveHistory)
        {
            return new()
            {
                StartTime = leaveHistory.StartTime,
                EndTime = leaveHistory.EndTime,
                Status = leaveHistory.Status,
                leaveEnum = leaveHistory.leaveEnum,
                NumberOfLeaves = leaveHistory.NumberOfLeaves,
                EmployeeName = leaveHistory.Leave.Employee.FirstName,
                CreatedDate = leaveHistory.CreatedDate
            };

        }
    }
}
