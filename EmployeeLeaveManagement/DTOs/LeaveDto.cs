using DomainEntity.Enum;

namespace DTOs
{
    public class LeaveDto : BaseDto
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }=DateTime.Now.AddDays(1);  
        public LeaveEnum LeaveEnum { get; set; }
        public int? EmployeeId { get; set; }
        public Status Status { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
    }
}
