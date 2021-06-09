using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> Repository;

        protected BaseService(IRepository<T> repository)
        {
            Repository = repository;
        }

        
        public virtual async Task<T> Create(T item)
        {
            return await Repository.Create(item);
        }

        
        public async Task<T> GetById(long id)
        {
            return await Repository.GetById(id);
        }

        public async Task<T> GetById(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Repository.GetById(id, includeProperties);
        }
        

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Repository.FirstOrDefault(predicate);
        }
        
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            return await Repository.FirstOrDefault(predicate, includeProperties);
        }

        
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Repository.GetAll();
        }

        public virtual async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Repository.GetAll(includeProperties);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await Repository.GetAll(filter);
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties
            )
        {
            return await Repository.GetAll(filter, orderBy, includeProperties);
        }


        public virtual async Task Update(T item)
        {
            await Repository.Update(item);
        }

        
        public async Task Remove(T item)
        {
            await Repository.Remove(item);
        }
    }
}
