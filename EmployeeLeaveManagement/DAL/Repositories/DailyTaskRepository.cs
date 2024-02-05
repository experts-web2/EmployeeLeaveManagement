using DAL.Interface;
using DomainEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DailyTaskRepository : GenericRepository<DailyTask>, IDailyTaskRepository
    {
        public DailyTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
