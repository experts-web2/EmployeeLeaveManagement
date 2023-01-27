using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly AppDbContext _dbContext;
        public AttendenceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddAttendence(AttendenceDto attendenceDto)
        {
            if (attendenceDto !=null)
            {
                try
                {
                    Attendence AttendenceEntity = ToEntity(attendenceDto);
                    _dbContext.Attendences.Add(AttendenceEntity);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                
            }
            return false;
          
        }
        public Attendence ToEntity(AttendenceDto attendenceDto)
        {
             Attendence attendence = new()
            {

                TimeIn = attendenceDto.TimeIn,
                Timeout = attendenceDto.Timeout,
                EmployeeId = attendenceDto.EmployeeId,
                Longitude = attendenceDto.Longitude,
                Latitude = attendenceDto.Latitude,
                hostName = AddHostName(),
                IpAddress=AddIpAddress()
                

            };
            return attendence;
        }
        public List<AttendenceDto> GetAllAttendences()
        {
            List<Attendence> attendences = _dbContext.Attendences.ToList();
            List<AttendenceDto> AttendenceDto = ToDtos(attendences);
            return AttendenceDto;
        }
        private List<AttendenceDto> ToDtos(List<Attendence> attendences)
        {
            try
            {
                List<AttendenceDto> attendenceDtos = new List<AttendenceDto>();
                foreach (var attendence in attendences)
                {
                    AttendenceDto attendenceDto = new AttendenceDto();
                    attendenceDto.ID= attendence.Id;
                    attendenceDto.TimeIn = attendence.TimeIn;
                    attendenceDto.Timeout = attendence.Timeout;
                    attendenceDto.hostName = attendence.hostName;
                    attendenceDto.IpAddress =attendence.IpAddress;
                    attendenceDto.Longitude = attendence.Longitude;
                    attendenceDto.Latitude = attendence.Latitude;
                    attendenceDto.EmployeeId= attendence.EmployeeId;

                    attendenceDtos.Add(attendenceDto);
                }
                return attendenceDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string AddHostName()
        {
            string HostName = Dns.GetHostName();
            return HostName;

        }
        public string AddIpAddress()
        {
            string HostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            return ipaddress[1].ToString() ;

        }
    }
}
