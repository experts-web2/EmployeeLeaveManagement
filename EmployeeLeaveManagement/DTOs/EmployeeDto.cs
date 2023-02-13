using DomainEntity.Enum;
using DomainEntity.Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class EmployeeDto : BaseDto
    {
        [Required]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        public string LastName { get; set; } = String.Empty;
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        public DateTime DateOfBrith { get; set; }
        [Required]
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public double CurrentSalary { get; set; }
        public List<Leave> leaves { get; set; } = new();

    }
}
