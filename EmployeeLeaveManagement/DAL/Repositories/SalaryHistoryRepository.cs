using DAL.Configrations;
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalaryHistoryRepository :GenericRepository<SalaryHistory>, ISalaryHistoryRepository
    {
        private AppDbContext dbContext;
        public SalaryHistoryRepository(AppDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
