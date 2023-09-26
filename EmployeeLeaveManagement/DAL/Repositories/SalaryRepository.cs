using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        private readonly AppDbContext _dbContext;
        public SalaryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }
    }
}
