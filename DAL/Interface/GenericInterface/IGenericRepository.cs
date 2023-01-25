using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.GenericInterface
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll ();
        T Add(T item);
        
        void deletebyid(int id);
        void update(T item);
        T GetByID(int id);
    }
}
