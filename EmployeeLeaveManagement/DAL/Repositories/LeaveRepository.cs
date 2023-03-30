using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LeaveRepository :GenericRepository<Leave>, ILeaveRepository
    {
        private readonly AppDbContext _dbContext;
        public LeaveRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
