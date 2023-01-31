
using DomainEntity.Pagination;
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
        PagedList<LeaveDto> GetAll(Pager pager);
        LeaveDto GetById(int id);
        LeaveDto Add(LeaveDto leave);
        void Delete(int id);
        LeaveDto Update(LeaveDto leave);
    }
}
