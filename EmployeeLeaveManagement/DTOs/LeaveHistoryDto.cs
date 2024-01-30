using DomainEntity.Enum;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LeaveHistoryDto : BaseDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public LeaveEnum leaveEnum { get; set; }
        public float NumberOfLeaves { get; set; }
        public string? EmployeeName { get; set; }

    }
}
