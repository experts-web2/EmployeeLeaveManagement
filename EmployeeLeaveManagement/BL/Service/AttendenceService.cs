using BL.Interface;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using DAL.Configrations;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace BL.Service
{
    public class AttendenceService : IAttendenceService
    {
        private readonly IAttendenceRepository _attendenceRepository;
        public AttendenceService(IAttendenceRepository attendenceRepository)
        {
            _attendenceRepository = attendenceRepository;
        }

        public bool AddAttendence(AttendenceDto attendenceDto)
        {
            if (attendenceDto != null)
            {
                try
                {
                    Attendence AttendenceEntity = ToEntity(attendenceDto);
                    _attendenceRepository.Add(AttendenceEntity);
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
                Id = attendenceDto.ID,
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
        public PagedList<AttendenceDto> GetAllAttendences(Pager paging, Expression<Func<Attendence, bool>> predicate = null)
        {

            if (predicate == null)
                predicate = PredicateBuilder.True<Attendence>();
            else
                predicate = predicate.And(predicate);
            var attendences = _attendenceRepository.GetAll().Include(x => x.Employee).AsQueryable();
            if (paging.StartDate?.Date != (DateTime.Now.Date) && paging.EndDate?.Date != DateTime.MinValue)
            {
                attendences = attendences.Where(x => x.AttendenceDate.Date <= paging.EndDate && x.AttendenceDate.Date >= paging.StartDate);
            }
            if (!string.IsNullOrEmpty(paging.Search))
            {
                attendences = attendences.Where(x => x.EmployeeId.ToString() == paging.Search);
            }
            else
                attendences = attendences.Where(x => x.AttendenceDate.Date <= paging.EndDate);

            var paginatedList = PagedList<Attendence>.ToPagedList(attendences.OrderByDescending(x=>x.AttendenceDate), paging.CurrentPage, paging.PageSize);
            var attendenceDto = ToDtos(paginatedList);
            return new PagedList<AttendenceDto>
                (attendenceDto, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);
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
        public bool DeleteAttendence(int id)
        {
            try
            {
                _attendenceRepository.deletebyid(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
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
            Attendence? FindAttendence = _attendenceRepository.Get(x => x.Id == id , x => x.Employee).FirstOrDefault();
            if (FindAttendence == null) return new AttendenceDto();
            AttendenceDto attendenceDto = SetAttendenceDto(FindAttendence);
            return attendenceDto;
        }
        public List<AttendenceDto> GetAttendencesByEmployeeId(int id)
        {
            var Attendances = _attendenceRepository.Get(x => x.EmployeeId == id, x => x.Employee);
            var attendenceDto = Attendances.Select(SetAttendenceDto);
            return attendenceDto.ToList();
        }
        private static AttendenceDto SetAttendenceDto(Attendence attendence)
        {
            if (attendence == null)
            {
                return null;
            }
            try
            {
                AttendenceDto attendenceDto = new()
                {
                    ID = attendence.Id,
                    AttendenceDate = attendence.AttendenceDate,
                    TimeIn = attendence.TimeIn,
                    Timeout = attendence.Timeout,
                    HostName = attendence.HostName,
                    IpAddress = attendence.IpAddress,
                    FirstName = attendence.Employee.FirstName,
                    LastName = attendence.Employee.LastName,
                    Longitude = attendence.Longitude,
                    EmployeeId = attendence.EmployeeId,
                    CreatedBy = attendence.CreatedBy,
                    CreatedDate = attendence.CreatedDate,
                    ModifiedBy = attendence.ModifiedBy,
                    ModifiedDate = attendence.ModifiedDate,

                };
                return attendenceDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateAttendence(AttendenceDto attendenceDto)
        {
            if (attendenceDto != null)
                try
                {

                    var attencence = _attendenceRepository.GetByID(attendenceDto.ID);
                    ToEntity(attencence,attendenceDto);

                    _attendenceRepository.update(attencence);
                    return true;
                }
                catch (Exception ex)
                {
                   
                }
            return false;
        }

        private void ToEntity(Attendence attendence, AttendenceDto attendenceDto)
        {
            try
            {
                attendence.AttendenceDate = attendenceDto.AttendenceDate;
                attendence.TimeIn = attendenceDto.TimeIn;
                attendence.Timeout = attendenceDto.Timeout;
                attendence.HostName = attendenceDto.HostName;
                attendence.IpAddress = attendenceDto.IpAddress;
                attendence.Longitude = attendenceDto.Longitude;
                attendence.EmployeeId = attendenceDto.EmployeeId;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public AttendenceDto GetAttendenceByEmployeeId(int employeeId)
        {
            try
            {
                Attendence? FindAttendence = _attendenceRepository.Get(x => x.EmployeeId == employeeId && x.AttendenceDate.Date == DateTime.Now.Date, x => x.Employee).FirstOrDefault();
                if (FindAttendence == null) return new AttendenceDto();
                AttendenceDto attendenceDto = SetAttendenceDto(FindAttendence);
                return attendenceDto;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<AttendenceDto> GetAttendenceByAlertDateAndEmployeeId(DateTime alertDate, int employeeId)
        {
            var attendence = await _attendenceRepository.Get(x => x.EmployeeId == employeeId && x.AttendenceDate.Date == alertDate.Date, x => x.Employee).FirstOrDefaultAsync();
            if (attendence == null) return new AttendenceDto();
            AttendenceDto attendenceDto=SetAttendenceDto(attendence);
            return attendenceDto;
        }
    }
}
