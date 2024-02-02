using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class DailyTimeSheet : EntityBase
    {
        public List<DailyTask>? DailyTasks { get; set; }
    }
}
