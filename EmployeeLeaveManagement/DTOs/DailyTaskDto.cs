using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DailyTaskDto : BaseDto
    {
        public string TaskName { get; set; }
        public float TaskTime { get; set; }
        public string Description { get; set; }

        public int EmployeeId { get; set; }
    }
}
