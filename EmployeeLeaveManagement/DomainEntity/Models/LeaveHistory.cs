using DomainEntity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class LeaveHistory : EntityBase
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public LeaveEnum leaveEnum { get; set; }
        public int NumberOfLeaves { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("Leave")]
        public int LeaveId { get; set; }
        public Leave? Leave { get; set; }
        
    }
}
