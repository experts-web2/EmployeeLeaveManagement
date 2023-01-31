﻿using DAL.Interface.GenericInterface;
using DomainEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
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
            var Findid = _table.FirstOrDefault(x => x.Id == id);
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

    }
}