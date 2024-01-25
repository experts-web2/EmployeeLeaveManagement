﻿using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface ILeaveHistoryService
    {
        public Task<List<LeaveHistory>> GetLeaveHistoryByEmployeeId();
    }
}
