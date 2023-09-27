﻿using DomainEntity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DomainEntity.Models
{
    public class Loan : EntityBase
    {
        public decimal LoanAmount { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime LoanDate { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        public bool Active { get; set; }
        public LoanType? LoanType { get; set; }
        public LoanStatus? LoanStatus { get; set; }
        public decimal RemainingAmount { get; set; }
        public InstallmentPlan? InstallmentPlan { get; set; }

        public Employee? Employee { get; set; }
        public ICollection<LoanInstallmentHistory>? LoanInstallmentHistories { get; set; }

    }
}
