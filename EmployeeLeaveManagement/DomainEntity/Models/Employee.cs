using DomainEntity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
        public string Address { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Leave> Leaves { get; set; }

    }
}
