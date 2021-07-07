using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CE.DataAccess.Models;

namespace CE.Service.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> Create(T item);

        Task<T> GetById(Guid id);
        Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includeProperties);

        Task Update(T item);

        Task Remove(T item);
    }
}
