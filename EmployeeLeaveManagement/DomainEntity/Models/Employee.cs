﻿using DomainEntity.Enum;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DomainEntity.Models
{
    public class Employee : EntityBase
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
        public double CurrentSalary { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Attendence> Attendences { get; set; }
        public ICollection<SalaryHistory> SalaryHistories { get; set; }
        public virtual User User { get; set; }
        public Salary? Salary { get; set; }
        public ICollection<Loan>? loans { get; set; }
    }
}
