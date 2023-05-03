
namespace DomainEntity.Models
{
    public class Alert : EntityBase
    {
        public DateTime AlertDate { get; set; }
        public string AlertType { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool isDeleted { get; set; }
    }
}
