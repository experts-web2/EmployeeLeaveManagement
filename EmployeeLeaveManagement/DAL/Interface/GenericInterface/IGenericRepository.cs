using DomainEntity;
using System.Linq.Expressions;

namespace DAL.Interface.GenericInterface
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll ();
        T Add(T item);
        void deletebyid(int id);
        void update(T item);
        T GetByID(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    }
}
