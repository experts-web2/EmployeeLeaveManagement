using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELM_DAL.Services.Interface
{
    public interface IDailyTimeSheetService
    {
        public Task<string> AddDailyTimeSheet(DailyTimeSheetDto dailyTimeSheetDto);
        public Task<List<DailyTimeSheetDto>> GetAllDailyTimeSheet();
    }
}
