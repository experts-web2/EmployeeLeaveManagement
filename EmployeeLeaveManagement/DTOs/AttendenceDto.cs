using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class AttendenceDto : BaseDto
    {


        [Required]
        public DateTime AttendenceDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Time)]
        public DateTime? TimeIn { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime? Timeout { get; set; }
        public string HostName { get; set; } = String.Empty;
        public string IpAddress { get; set; } = String.Empty;
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
       
    }
}
