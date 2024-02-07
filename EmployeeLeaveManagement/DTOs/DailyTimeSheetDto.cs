using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DailyTimeSheetDto : BaseDto
    {
        public float TotalTime { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? TotalTasks { get; set; } 
        public List<DailyTaskDto>? dailyTaskDtos { get; set; }
    }
}
