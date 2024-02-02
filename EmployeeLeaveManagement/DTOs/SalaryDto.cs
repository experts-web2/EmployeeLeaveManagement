using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class SalaryDto : BaseDto
    {
        public decimal CurrentSalary { get; set; }
        public decimal LeaveDeduction { get; set; }
        public decimal GeneralDeduction { get; set; }
        public decimal LoanDeduction { get; set; }
        public decimal TotalDedection { get; set; }
        public decimal Perks { get; set; }
        public decimal TotalSalary { get; set; }
        public int? EmployeeId { get; set; } 
        public string EmployeeName { get; set; }
        public List<SalaryHistory>? SalaryHistories { get; set; }
        public float TotalLeaves { get; set; }
        public decimal RemainingLoan { get; set;}
    }
}
