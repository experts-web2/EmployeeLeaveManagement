using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DailyTimeSheetRepository : GenericRepository<DailyTimeSheet>, IDailyTimeSheetRepository
    {
        public DailyTimeSheetRepository(AppDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
