using DomainEntity.Enum;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LoanDto : BaseDto
    {
        public decimal LoanAmount { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public int EmployeeId { get; set; }

        public bool Active { get; set; }
        public LoanType? LoanType { get; set; }
        public LoanStatus? LoanStatus { get; set; }
        public decimal RemainingAmount { get; set; }
        public InstallmentPlan? InstallmentPlan { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
