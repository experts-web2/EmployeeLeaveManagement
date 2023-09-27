using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class LoanInstallmentHistory : EntityBase
    {
        public decimal InstallmentAmount { get; set; }

        [ForeignKey(nameof(Loan))]
        public int LoanId { get; set; }
        public Loan? Loan { get; set; }

    }
}
