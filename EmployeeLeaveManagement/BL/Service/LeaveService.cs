using BL.Interface;
using DAL.Interface;
using DAL.Interface.GenericInterface;
using DAL.Repositories;
using DomainEntity.Enum;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BL.Service
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        public LeaveService( ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }
        public LeaveDto Add(LeaveDto leaveDto)
        {
            if (leaveDto == null)
            {
                return null;
            }
            try
            {
                Leave leaveEntity = ToEntity(leaveDto);
                _leaveRepository.Add(leaveEntity);
                return leaveDto;
            }
            catch (Exception)
            {

                throw;
            }
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
        public List<Leave> GetAllEmployeesLeaves()
        {
            try
            {
                return _leaveRepository.GetAll().ToList();
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
                    employeeDtos.Add(leaveDto);
                }
                return employeeDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(LeaveDto leaveDto)
        {
            try
            {
                var leave=_leaveRepository.GetByID(leaveDto.ID);
                ToEntity(leave,leaveDto);
                _leaveRepository.update(leave);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ToEntity(Leave leave, LeaveDto leaveDto)
        {
            try
            {
                leave.EmployeeId = leaveDto.EmployeeId;
                leave.StartTime = leaveDto.StartTime;
                leave.EndTime = leaveDto.EndTime;
                leave.Status = leaveDto.Status;
                leave.leaveEnum = leaveDto.LeaveEnum;
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
                var dtos = ToDtos( leave.ToList());
                return dtos;
            }
            catch (Exception)

            {
                throw;
            }
        }

    }
}
