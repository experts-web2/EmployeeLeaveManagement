using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LoanInstallmentHistoryRepository : GenericRepository<LoanInstallmentHistory>, ILoanInstallmentHistoryRepository
    {
        private readonly AppDbContext _dbContext;
        public LoanInstallmentHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }
    }
}
