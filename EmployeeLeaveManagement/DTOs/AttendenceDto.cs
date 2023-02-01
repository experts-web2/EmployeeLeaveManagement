using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AttendenceDto : BaseDto
    {


        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public DateTime TimeIn { get; set; }
        [Required]
        public DateTime Timeout { get; set; }
        public string hostName { get; set; } = String.Empty;
        public string IpAddress { get; set; } = String.Empty;
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public int EmployeeId { get; set; }
       
       
    }
}
