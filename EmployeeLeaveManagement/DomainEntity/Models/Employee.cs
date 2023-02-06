using DomainEntity.Enum;
using System.ComponentModel.DataAnnotations;

namespace DomainEntity.Models
{
    public class Employee : BaseEntity
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBrith { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Attendence> Attendences { get; set; }
    }
}
