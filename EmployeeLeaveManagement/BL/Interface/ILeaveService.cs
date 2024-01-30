﻿using DomainEntity.Models;
using DTOs;
using ELM.Helper;

namespace BL.Interface
{
    public interface ILeaveService
    {
        PagedList<LeaveDto> GetAll(Pager pager);
        LeaveDto GetById(int id);
        //LeaveDto Add(LeaveDto leave);
        void Delete(int id);
        Leave Update(LeaveDto leave, Leave DbLeave = null);
        Task<List<LeaveDto>> GetLeavesByEmployeeID(int employeeId);
        void AddorUpdate(LeaveDto leaveDto);
    }
}
