using DAL.Interface;
using DAL.Interface.GenericInterface;
using DomainEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _DbContext;
        private readonly DbSet<T> _table;
        public GenericRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
            _table = _DbContext.Set<T>();
        }

        public T Add(T item)
        {
            var result = _table.Add(item);
            _DbContext.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> items)
        {
            _table.AddRange(items);
            _DbContext.SaveChanges();
            return items;
        }

        public void deletebyid(int id)
        {
            var DeletedObj = _table.FirstOrDefault(x => x.Id == id);
            if (DeletedObj == null)
                return;
            _table.Remove(DeletedObj);
            _DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public T GetByID(int id)
        {
            var Findid = _table.Find(id);
            if (Findid != null)
            {
                return Findid;
            }
            return null;
        }
        public void update(T item)
        {
            _DbContext.Update(item);
            _DbContext.SaveChanges();
        }

        public  IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return Query().Where(predicate).Includes(includes);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private IQueryable<T> Query()
        {
            return Entity().AsQueryable<T>();
        }

        private DbSet<T> Entity()
        {
            return _DbContext.Set<T>();
        }

        public void DeleteAlertByEmployeeId(Expression<Func<T, bool>> predicate)
        {
            var alert = _table.FirstOrDefault(predicate);
            if(alert != null)
            {
                _DbContext.Remove(alert);
                _DbContext.SaveChanges();
            }
        }
    }

    public static class IQueryableExtension
    {
        public static IQueryable<T> Includes<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes is null || !includes.Any())
                return query;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}