using BL.Interface;
using DAL.Interface;
using DAL.Interface.GenericInterface;
using DomainEntity.Enum;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace BL.Service
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILeaveHistoryRepository _leaveHistoryRepository;
        public LeaveService(ILeaveRepository leaveRepository, ILeaveHistoryRepository leaveHistoryRepository)
        {
            _leaveRepository = leaveRepository;
            _leaveHistoryRepository = leaveHistoryRepository;
        }
        //public LeaveDto Add(LeaveDto leaveDto)
        //{
        //    if (leaveDto == null)
        //    {
        //        return new();
        //    }
        //    try
        //    {
        //        Leave leaveEntity = ToEntity(leaveDto);
        //        _leaveRepository.Add(leaveEntity);
        //        return leaveDto;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public void AddorUpdate(LeaveDto leaveDto)
        {
            if (!(leaveDto.EndTime >= DateTime.Today))
            {
                return;
            }
            Leave leaveResponse = new();
            LeaveHistory? DbleaveHistory = new();
            TimeSpan DayDifference = leaveDto.EndTime - leaveDto.StartTime ;
            try
            {
                var existedLeavesOfEmployee = _leaveRepository.Get(x => x.EmployeeId == leaveDto.EmployeeId, y=>y.Employee).FirstOrDefault();

                if (existedLeavesOfEmployee != null) 
                {
                    if (leaveDto.Status == Status.Approved)
                    {
                        leaveResponse = Update(leaveDto, existedLeavesOfEmployee);
                        DbleaveHistory = _leaveHistoryRepository.Get(x => x.EmployeeId == leaveDto.EmployeeId && x.LeaveId == leaveResponse.Id, y=>y.Leave).OrderByDescending(x=>x.CreatedDate).FirstOrDefault();
                        if (DbleaveHistory != null)
                        {
                            DbleaveHistory.Status = Status.Approved;
                           // DbleaveHistory.NumberOfLeaves = leaveResponse.NumberOfLeaves - DbleaveHistory.NumberOfLeaves;
                            _leaveHistoryRepository.update(DbleaveHistory);
                        }
                    }
                    else if (leaveDto.Status == Status.Cancel)
                    {
                        leaveResponse = Update(leaveDto);
                        DbleaveHistory = _leaveHistoryRepository.Get(x => x.EmployeeId == leaveDto.EmployeeId && x.LeaveId == leaveDto.ID, y => y.Leave).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                        if (DbleaveHistory != null)
                        {
                            DbleaveHistory.Status = Status.Cancel;
                            _leaveHistoryRepository.update(DbleaveHistory);
                        }
                    }
                    else
                    {
                        if (leaveDto.StartTime != existedLeavesOfEmployee.StartTime && leaveDto.EndTime != existedLeavesOfEmployee.EndTime)
                        {
                            leaveResponse = setLeaveEntity(leaveDto, existedLeavesOfEmployee);
                            _leaveRepository.update(leaveResponse);
                            _leaveHistoryRepository.Add(new LeaveHistory() { StartTime = leaveDto.StartTime, EndTime = leaveDto.EndTime, CreatedDate = leaveDto.CreatedDate, EmployeeId = leaveDto.EmployeeId!.Value, Status = leaveDto.Status, leaveEnum = leaveDto.LeaveEnum, LeaveId = leaveResponse.Id, NumberOfLeaves = FindDayDifference(DayDifference.Days, leaveDto.LeaveEnum) });

                        }
                        else
                            return;
                    }

                }
                else
                {
                    leaveResponse = setLeaveEntity(leaveDto);
                    _leaveRepository.Add(leaveResponse);
                    _leaveHistoryRepository.Add(new LeaveHistory() { StartTime = leaveDto.StartTime, EndTime = leaveDto.EndTime, CreatedDate = leaveDto.CreatedDate, EmployeeId = leaveDto.EmployeeId!.Value, Status = leaveDto.Status, leaveEnum = leaveDto.LeaveEnum, LeaveId = leaveResponse.Id , NumberOfLeaves = FindDayDifference(DayDifference.Days, leaveDto.LeaveEnum) });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private float FindDayDifference(int Days , LeaveEnum leaveEnum)
        {
            if (Days == 0 && leaveEnum == LeaveEnum.Half_Leave)
                return 0.5f;
            else if (Days == 0)
                return Days + 1;
            else if( Days < 0)
                return 0;
            else
                return Days + 1;
        } 

        private Leave setLeaveEntity(LeaveDto leaveDto, Leave DbLeave = null)
        {
            TimeSpan DayDifference = leaveDto.EndTime - leaveDto.StartTime; 
            if (DbLeave == null)
            {
                DbLeave = new Leave();
                DbLeave.CreatedDate = DateTime.Now;
                //if (DayDifference.Days == 0)
                //{
                //    DbLeave.NumberOfLeaves = DayDifference.Days + 1;
                //}
                //else
                //    DbLeave.NumberOfLeaves = DayDifference.Days + 1;

            }
            else
            {
                DbLeave.ModifiedDate = DateTime.Now;
               // DbLeave.NumberOfLeaves += DayDifference.Days;
            }
            DbLeave.EmployeeId = leaveDto.EmployeeId;
            DbLeave.StartTime = leaveDto.StartTime;
            DbLeave.EndTime = leaveDto.EndTime;
            DbLeave.Status = Status.Under_Approval;
            DbLeave.leaveEnum = leaveDto.LeaveEnum;
            return DbLeave;
        }

        private Leave ToEntity(LeaveDto leaveDto)
        {
            Leave leave = new()
            {
                EmployeeId = leaveDto.EmployeeId,
                Id = leaveDto.ID,
                StartTime = leaveDto.StartTime,
                EndTime = leaveDto.EndTime,
                Status = leaveDto.Status,

                leaveEnum = leaveDto.LeaveEnum

            };
            return leave;
        }
        public void Delete(int id)
        {
            try
            {
                _leaveRepository.deletebyid(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public LeaveDto GetById(int id)
        {
            var GetId = _leaveRepository.GetByID(id);
            return SetLeaveDto(GetId);
        }
        private static LeaveDto SetLeaveDto(Leave leave)
        {
            if (leave == null)
            {
                return null;
            }
            try
            {
                LeaveDto leaveDto = new LeaveDto()
                {
                    ID = leave.Id,
                    StartTime = leave.StartTime,

                    EndTime = leave.EndTime,
                    Status = leave.Status,
                    LeaveEnum = leave.leaveEnum,
                    EmployeeId = leave.EmployeeId

                };
                return leaveDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PagedList<LeaveDto> GetAll(Pager pager)
        {
            try
            {
                IQueryable<Leave> allEmpLeaves = _leaveRepository.GetAll().Include(x => x.Employee);
                if (!string.IsNullOrEmpty(pager.Search))
                {
                    allEmpLeaves = allEmpLeaves.
                        Where(x => x.Employee.FirstName.Contains(pager.Search.Trim()) ||
                              x.Employee.LastName.Contains(pager.Search.Trim()) ||
                              x.Employee.Email.Contains(pager.Search.Trim()) ||
                              x.Employee.Address.Contains(pager.Search.Trim()));
                }
                var paginatedList = PagedList<Leave>.ToPagedList(allEmpLeaves, pager.CurrentPage, pager.PageSize);
                var leaveDtos = ToDtos(paginatedList);
                return new PagedList<LeaveDto>
               (leaveDtos, paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private List<LeaveDto> ToDtos(IEnumerable<Leave> leaves)
        {
            try
            {
                List<LeaveDto> employeeDtos = new List<LeaveDto>();
                foreach (var leave in leaves)
                {
                    LeaveDto leaveDto = new LeaveDto();
                    leaveDto.ID = leave.Id;
                    leaveDto.StartTime = leave.StartTime;
                    leaveDto.EndTime = leave.EndTime;
                    leaveDto.Status = leave.Status;
                    leaveDto.LeaveEnum = leave.leaveEnum;
                    leaveDto.EmployeeId = leave.EmployeeId;
                    leaveDto.EmployeeName = leave.Employee.FirstName;
                    leaveDto.TotalLeaves = leave.NumberOfLeaves;
                    employeeDtos.Add(leaveDto);
                }
                return employeeDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Leave Update(LeaveDto leaveDto , Leave dbLeave = null)
        {
            try
            {
                if (leaveDto.ID <= 0)
                {
                    leaveDto.ID = dbLeave.Id;
                }
                var leave=_leaveRepository.GetByID(leaveDto.ID);
                ToEntity(leave,leaveDto);
                _leaveRepository.update(leave);
                return leave;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ToEntity(Leave leave, LeaveDto leaveDto)
        {
            TimeSpan dayDifference = leave.EndTime - leave.StartTime;
          
            try
            {
                leave.StartTime = leaveDto.StartTime;
                leave.EndTime = leaveDto.EndTime;
                leave.Status = leaveDto.Status;
                if (leaveDto.Status == Status.Cancel)
                    leave.NumberOfLeaves += 0;
                else if (LeaveEnum.Half_Leave == leaveDto.LeaveEnum)
                    leave.NumberOfLeaves += 0.5f;
                else if (dayDifference.Days == 0)
                {
                    leave.NumberOfLeaves += 1;
                }
                else
                    leave.NumberOfLeaves += dayDifference.Days + 1;

            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public async Task<List<LeaveDto>> GetLeavesByEmployeeID(int employeeId)
        {
            try
            {
                var leave = _leaveRepository.Get(x => x.EmployeeId == employeeId, x => x.Employee);
                var dtos = ToDtos(leave.ToList());
                return dtos;
            }
            catch (Exception)

            {
                throw;
            }
        }

    }
}
