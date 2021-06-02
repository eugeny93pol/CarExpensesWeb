using CE.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CE.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //create
        public async Task<T> Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        //read one
        public async Task<T> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetById(long id, params string[] includeProperties)
        {
            return await Include(includeProperties).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<T> GetAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        //read many
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }
        
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties
            )
        {
            var query = Include(includeProperties);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties
            )
        {
            var query = Include(includeProperties);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        //update
        public async Task Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //delete
        public async Task Remove(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        //private
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private IQueryable<T> Include(params string[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();

            foreach (var property in includeProperties)
            {
                var propertyName = char.ToUpper(property[0]) + property.Substring(1).ToLower();
                query = query.Include(propertyName);
            }

            return query;
        }
    }
}
