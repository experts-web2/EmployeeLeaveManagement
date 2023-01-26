
using DomainEntity.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntity.Models
{
    public class Leave : BaseEntity
    {
        public DateTime StartTime { get; set; }= DateTime.Now;
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public LeaveEnum leaveEnum { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
     
    }
}
