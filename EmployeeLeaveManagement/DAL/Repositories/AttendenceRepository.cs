
using DAL.Interface;
using DomainEntity.Models;


namespace DAL.Repositories
{
    public class AttendenceRepository : GenericRepository<Attendence>, IAttendenceRepository
    {
        private readonly AppDbContext _dbContext;
        public AttendenceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
