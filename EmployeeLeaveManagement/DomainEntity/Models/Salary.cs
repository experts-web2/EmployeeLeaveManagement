using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class Salary : EntityBase
    {
        public decimal CurrentSalary { get; set; }
        public decimal LeaveDeduction { get; set; }
        public decimal GeneralDeduction { get; set; }
        public decimal LoanDeduction { get; set; }
        public decimal TotalDedection { get; set; }
        public decimal Perks { get; set; }
        public decimal TotalSalary { get; set; }
        public List<SalaryHistory>? SalaryHistories { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
