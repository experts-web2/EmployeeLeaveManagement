using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LeaveHistoryRepository : GenericRepository<LeaveHistory>, ILeaveHistoryRepository
    {
        private readonly AppDbContext _dbContext;
        public LeaveHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
