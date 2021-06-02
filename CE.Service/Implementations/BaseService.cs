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

        //Create
        public async Task<T> Create(T item)
        {
            return await Repository.Create(item);
        }

        //Get one
        public async Task<T> GetById(long id)
        {
            return await Repository.GetById(id);
        }

        public async Task<T> GetById(long id, params string[] includeProperties)
        {
            return await Repository.GetById(id, includeProperties);
        }
        
        public async Task<T> GetAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return await Repository.GetAsNoTracking(predicate);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Repository.FirstOrDefault(predicate);
        }

        //Get many
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Repository.GetAll(includeProperties);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await Repository.GetAll(filter);
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
            return await Repository.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties
            )
        {
            return await Repository.GetAll(filter, orderBy, includeProperties);
        }

        //update
        public async Task Update(T item)
        {
            await Repository.Update(item);
        }

        //delete
        public async Task Remove(T item)
        {
            await Repository.Remove(item);
        }

    }
}
