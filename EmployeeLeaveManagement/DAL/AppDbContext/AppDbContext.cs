
using DomainEntity;
using DomainEntity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;

namespace DAL
{
  
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public override int SaveChanges()
        {

            SetBaseEntity();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseEntity();

            return base.SaveChangesAsync(cancellationToken);
        }
        private void SetBaseEntity()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is EntityBase && (e.State == EntityState.Added|| e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                var currentUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((EntityBase)entityEntry.Entity).CreatedBy = currentUser;
                }
                if(entityEntry.State == EntityState.Modified)
                {
                    ((EntityBase)entityEntry.Entity).ModifiedDate = DateTime.Now;
                    ((EntityBase)entityEntry.Entity).ModifiedBy = currentUser;

                }
            }
        }
    }
}
