
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntity.Models
{
    public class Attendence : BaseEntity
    {
        [Required]
        public DateTime AttendenceDate { get; set; }
        [Required]
        public DateTime? TimeIn { get; set; }
        public DateTime? Timeout { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
