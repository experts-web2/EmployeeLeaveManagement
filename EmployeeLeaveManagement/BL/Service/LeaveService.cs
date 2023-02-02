﻿using BL.Interface;
using DAL.Interface.GenericInterface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class LeaveService : ILeaveService
    {
        private readonly IGenericRepository<Leave> _genericRepository;

        public LeaveService(IGenericRepository<Leave> genericRepository)
        {
            _genericRepository = genericRepository;
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
                _genericRepository.Add(leaveEntity);
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
                _genericRepository.deletebyid(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public LeaveDto GetById(int id)
        {
            var GetId = _genericRepository.GetByID(id);
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
        public List<LeaveDto> GetAll()
        {
            try
            {
                var Leaves = _genericRepository.GetAll().Include(x => x.Employee).ToList();
                List<LeaveDto> leaveDtos = ToDtos(Leaves);
                return leaveDtos;
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
        public LeaveDto Update(LeaveDto leave)
        {
            if (leave == null)
            {
                return null;
            }
            try
            {
                _genericRepository.update(ToEntity(leave));
                return leave;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
