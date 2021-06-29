﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CE.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        
        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).FirstOrDefaultAsync(q => q.Id == id);
        }


        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).FirstOrDefaultAsync(predicate);
        }


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

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }


        public async Task Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        
        public async Task Remove(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, 
                (current, includeProperty) => current.Include(includeProperty));
        }
    }
}