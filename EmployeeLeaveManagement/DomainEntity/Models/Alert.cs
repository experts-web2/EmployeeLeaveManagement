using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class Alert : BaseEntity
    {
        public DateTime AlertDate { get; set; }
        public string AlertType { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
