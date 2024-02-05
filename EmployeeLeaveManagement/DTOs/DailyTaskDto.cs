using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DailyTaskDto : BaseDto
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public float TaskTime { get; set; }
        [Required]
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
