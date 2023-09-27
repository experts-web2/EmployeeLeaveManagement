using DAL.Interface;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly AppDbContext _dbContext;
        public LoanRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public Loan? GetLoanWithEmployeeId(int employeeId)
        {
           return _dbContext.Loans.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
        }
    }
}
