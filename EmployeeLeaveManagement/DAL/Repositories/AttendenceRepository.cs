using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
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
            if (attendenceDto != null)
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
                Id=attendenceDto.ID,
                AttendenceDate = attendenceDto.AttendenceDate,
                TimeIn = attendenceDto.TimeIn,
                Timeout = attendenceDto.Timeout,
                EmployeeId = attendenceDto.EmployeeId,
                Longitude = attendenceDto.Longitude,
                Latitude = attendenceDto.Latitude,
                HostName = AddHostName(),
                IpAddress = AddIpAddress()
            };
            return attendence;
        }
        public List<AttendenceDto> GetAllAttendences()
        {
            var attendences = _dbContext.Attendences.Include(x=>x.Employee).ToList();
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
                    AttendenceDto attendenceDto = new()
                    {
                        ID = attendence.Id,
                        AttendenceDate = attendence.AttendenceDate,
                        TimeIn = attendence.TimeIn,
                        Timeout = attendence.Timeout,
                        HostName = attendence.HostName,
                        IpAddress = attendence.IpAddress,
                        Longitude = attendence.Longitude,
                        Latitude = attendence.Latitude,
                        FirstName = attendence.Employee.FirstName,
                        LastName = attendence.Employee.LastName,

                    };
                    attendenceDtos.Add(attendenceDto);
                }
                return attendenceDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DeleteAttendence(int id)
        {
            try
            {
                var Deleted = _dbContext.Attendences.FirstOrDefault(x => x.Id == id);
                if (Deleted != null)
                {
                    _dbContext.Remove(Deleted);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
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
            return ipaddress[1].ToString();

        }

        public AttendenceDto GetById(int id)
        {
            var FindAttendence = _dbContext.Attendences.Include(x => x.Employee).FirstOrDefault(x => x.Id == id);
            AttendenceDto attendenceDto = SetAttendenceDto(FindAttendence);
            return attendenceDto;
        }
        private static AttendenceDto SetAttendenceDto(Attendence attendence)
        {
            if (attendence == null)
            {
                return null;
            }
            try
            {
               AttendenceDto employeeDto = new()
                {
                    ID = attendence.Id,
                    AttendenceDate = attendence.AttendenceDate,
                    TimeIn = attendence.TimeIn,
                    Timeout = attendence.Timeout,
                    HostName = attendence.HostName,
                    IpAddress = attendence.IpAddress,
                    Longitude = attendence.Longitude,
                   EmployeeId=attendence.Id

                };
                return employeeDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Update(AttendenceDto attendenceDto)
        {
            if (attendenceDto != null)
                try
                {
                    var Updated = ToEntity(attendenceDto);
                    _dbContext.Update(Updated);
                    _dbContext.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
        }
    }
}
