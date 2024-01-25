
using DomainEntity.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntity.Models
{
    public class Leave : EntityBase
    {
        public DateTime StartTime { get; set; }= DateTime.Now;
        public DateTime EndTime { get; set; }=DateTime.Now.AddDays(1);
        public Status Status { get; set; }
        public LeaveEnum leaveEnum { get; set; }
        public int NumberOfLeaves { get; set; }
        //public TimeSpan Duration { get; set; }
        //public string? Comment { get; set; }
        //public int RemainingLeaves { get; set; }
        //public int Available { get; set; }
        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<LeaveHistory>? LeaveHistories { get; set; }
     
    }
}
