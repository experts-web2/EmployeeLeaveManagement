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
        public DateTime TimeIn { get; set; }
        [Required]
        public DateTime Timeout { get; set; }
        [Required]

        public string hostName { get; set; }
        [Required]

        public string IpAddress { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public int EmployeeId { get; set; }
       
    }
}
