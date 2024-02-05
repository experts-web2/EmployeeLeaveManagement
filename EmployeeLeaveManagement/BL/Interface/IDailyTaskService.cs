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
    public interface IDailyTaskService
    {
        DailyTaskDto AddDailyTask(DailyTaskDto dailyTaskDto);
        DailyTaskDto Update(DailyTaskDto dailyTaskDto);
        void DeleteDailyTask(int id);
        DailyTaskDto GetById(int id);
        List<DailyTaskDto> GetAllDailyTask();
        List<DailyTaskDto> GetAllDailyTaskByEmployeeId(int employeeId);
    }
}
