using CE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CE.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Create(T item);

        Task<T> GetById(long id);
        Task<T> GetById(long id, params string[] includeProperties);
        Task<T> GetAsNoTracking(Expression<Func<T, bool>> filter);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties
            );

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties
            );

        Task Update(T item);

        Task Remove(T item);
    }
}
