using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly AppDbContext _dbContext;
        public LeaveRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Addleave(LeaveDto leaveDto)
        {
            Leave leaveEntity = ToEntity(leaveDto);
           _dbContext.Leaves.Add(leaveEntity);
            _dbContext.SaveChanges();
        }
        private Leave ToEntity(LeaveDto leaveDto)
        {
            Leave leave = new()
            {

                StartTime = leaveDto.StartTime,
                EndTime = leaveDto.EndTime,
                Status = leaveDto.Status,
                Id = leaveDto.ID,
                leaveEnum = leaveDto.LeaveEnum

             };
            return leave;
        }

        public List<LeaveDto> GetAllLeave()
        {
            throw new NotImplementedException();
        }
       
    }
}
