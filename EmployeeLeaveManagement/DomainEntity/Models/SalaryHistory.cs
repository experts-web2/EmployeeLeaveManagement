using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class SalaryHistory:EntityBase
    {
       
        [Required]
        public double NewSalary { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime IncrementDate { get; set; }
        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
