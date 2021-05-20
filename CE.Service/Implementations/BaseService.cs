using CE.DataAccess;
using CE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CE.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        //Create
        public async Task<T> Create(T item)
        {
            return await _repository.Create(item);
        }

        //Get one
        public async Task<T> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T> GetById(long id, params string[] includeProperties)
        {
            return await _repository.GetById(id, includeProperties);
        }
        
        public async Task<T> GetAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetAsNoTracking(predicate);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FirstOrDefault(predicate);
        }

        //Get many
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return await _repository.GetAll(includeProperties);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await _repository.GetAll(filter);
        }

        /* 
         * var users = await _userService.GetAll(orderBy: q => q.OrderByDescending(u => u.Id), includeProperties: u => u.Cars);
         * var users = await _userService.GetAll(orderBy: q => q.OrderByDescending(u => u.Id));
         * var users = await _userService.GetAll(null, q => q.OrderByDescending(u => u.Id), u => u.Cars);
         */

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties
            )
        {
            return await _repository.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties
            )
        {
            return await _repository.GetAll(filter, orderBy, includeProperties);
        }

        //update
        public async Task Update(T item)
        {
            await _repository.Update(item);
        }

        //delete
        public async Task Remove(T item)
        {
            await _repository.Remove(item);
        }

    }
}
