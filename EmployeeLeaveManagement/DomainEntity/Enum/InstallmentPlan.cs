using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Enum
{
    public enum InstallmentPlan
    {
        [Display(Name = "Monthly Plan")]
        MonthlyPlan = 1,
        [Display(Name = "Three Month Plan")]
        ThreeMonthPlan = 3,
        [Display(Name = "Six Month Plan")]
        SixMonthPlan = 6,
        [Display(Name = "Annual Plan")]
        AnnualPlan = 12,
    }
}
