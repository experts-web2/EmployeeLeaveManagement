﻿
using DTOs;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ILeaveService
    {
        List<LeaveDto> GetAll();
        LeaveDto Get(int id);
        LeaveDto Add(LeaveDto leave);
        void Delete(int id);
        LeaveDto Update(LeaveDto leave);
    }
}
