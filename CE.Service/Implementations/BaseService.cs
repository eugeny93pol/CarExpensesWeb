using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly GenericRepository<T> GenericRepository;

        protected BaseService(IGenericRepository<T> genericRepository)
        {
            GenericRepository = genericRepository as GenericRepository<T>;
        }

        
        public virtual async Task<T> Create(T item)
        {
            return await GenericRepository.Create(item);
        }

        
        public async Task<T> GetById(Guid id)
        {
            return await GenericRepository.GetById(id);
        }

        public async Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GenericRepository.GetById(id, includeProperties);
        }
        

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await GenericRepository.FirstOrDefault(predicate);
        }
        
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            return await GenericRepository.FirstOrDefault(predicate, includeProperties);
        }

        
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await GenericRepository.GetAll();
        }

        public virtual async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return await GenericRepository.GetAll(includeProperties);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await GenericRepository.GetAll(filter);
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties
            )
        {
            return await GenericRepository.GetAll(filter, orderBy, includeProperties);
        }


        public virtual async Task Update(T item)
        {
            await GenericRepository.Update(item);
        }

        
        public async Task Remove(T item)
        {
            await GenericRepository.Remove(item);
        }
    }
}
