using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class Attendence : BaseEntity
    {
        [Required]
        public DateTime TimeIn { get; set; }
        [Required]
        public DateTime  Timeout{ get; set; }
        [Required]
       
        public string hostName { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [ForeignKey("Employee")]
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}
