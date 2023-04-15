using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AlertDto
    {
        public DateTime AlertDate { get; set; }
        public string AlertType { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
