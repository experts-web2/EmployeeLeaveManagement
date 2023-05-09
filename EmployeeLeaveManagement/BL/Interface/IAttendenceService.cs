using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IAttendenceService
    {
        PagedList<AttendenceDto> GetAllAttendences(Pager paging, Expression<Func<Attendence, bool>> predicate = null);
        List<AttendenceDto> GetAllAttendencesWithoutPaging();
        //  PagedList<AttendenceDto> GetAttendence(Pager paging);
        bool AddAttendence(AttendenceDto attendenceDto);
        bool DeleteAttendence(int id);
        AttendenceDto GetById(int id);
        PagedList<AttendenceDto> GetAttendencesByEmployeeId(int id,Pager paging);
        AttendenceDto GetAttendenceByEmployeeId(int employeeId, DateTime attendenceDate);
        bool UpdateAttendence(AttendenceDto attendenceDto);
        Task<AttendenceDto> GetAttendenceByAlertDateAndEmployeeId(DateTime alertDate,int employeeId);
    }
}
