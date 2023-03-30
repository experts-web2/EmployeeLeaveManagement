using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
   public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
