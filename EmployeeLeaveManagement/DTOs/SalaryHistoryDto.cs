using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public  class SalaryHistoryDto:BaseDto
    {
        [Required]
        public double NewSalary { get; set; }
        [Required]
        public DateTime IncrementDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
