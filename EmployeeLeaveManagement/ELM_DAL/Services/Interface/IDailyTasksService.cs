using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface IDailyTasksService
    {
        public Task<string> AddDailyTask(DailyTaskDto dailyTaskDto);
        public Task<List<DailyTaskDto>> GetAllDailyTask();
    }
}
