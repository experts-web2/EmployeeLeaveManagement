using DAL.Interface.GenericInterface;
using DAL.Repositories;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ILoanInstallmentHistoryRepository : IGenericRepository<LoanInstallmentHistory>
    {
    }
}
