using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class Attendence : BaseEntity
    {
        public DateTime TimeIn { get; set; }
        public DateTime  Timeout{ get; set; }
       
        public string Designation { get; set; }
       
        public string hostName { get; set; }
        public string IpAddress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}
