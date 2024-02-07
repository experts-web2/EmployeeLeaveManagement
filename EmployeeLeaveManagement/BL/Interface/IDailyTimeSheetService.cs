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
    public interface IDailyTimeSheetService
    {
        DailyTimeSheetDto AddDailyTimeSheet(DailyTimeSheetDto dailyTaskDto);
        DailyTimeSheetDto UpdateDailyTimeSheet(DailyTimeSheetDto dailyTaskDto);
        void DeleteDailyTimesheet(int id);
        DailyTimeSheetDto GetById(int id);
        List<DailyTimeSheetDto> GetAllDailyTimeSheet();
        List<DailyTimeSheetDto> GetAllDailyTimeSheetByEmployeeId(int employeeId);
        public PagedList<DailyTimeSheetDto> GetAllDailyTimeSheetWithFilter(Pager paging, string EmployeeId = null, Expression<Func<DailyTimeSheet, bool>> predicate = null);
    }
}
