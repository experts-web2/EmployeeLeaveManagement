using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AttendenceDto : BaseDto
    {
        public DateTime TimeIn { get; set; }

        public DateTime Timeout { get; set; }

        public string Designation { get; set; }

        public string hostName { get; set; }
        public string IpAddress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int EmployeeId { get; set; }
       
    }
}
