using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LoanInstallmentHistoryDto : BaseDto
    {
        public decimal InstallmentAmount { get; set; }
        public int LoanId { get; set; }
    }
}
