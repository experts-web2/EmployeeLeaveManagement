using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Enum
{
    public enum LoanType
    {
        [Display(Name ="Advance Salary Loan")]
        AdvanceSalaryLoan = 1,
        [Display(Name = "Personal Loan")]
        PersonalLoan = 2,
        
    }
}
