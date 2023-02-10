using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _dbContext;
        public JobService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AttendenceDto> GetAllAttendences()
        {
            var attendences = _dbContext.Attendences.Include(x => x.Employee).ToList();
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
        public List<Employee> GetAbsentEmployee()
        {

            var AbsentEmployee = (from emp in _dbContext.Employees
                                 join a in _dbContext.Attendences.Where(x => x.AttendenceDate.Date.Equals(DateTime.Now.Date)) on emp.Id equals a.EmployeeId
                                 into employeeAtendence
                                 from attendence in employeeAtendence.DefaultIfEmpty()
                                 where attendence == null
                                 
                                 select emp).ToList();
            return AbsentEmployee;
        }


    }
}

