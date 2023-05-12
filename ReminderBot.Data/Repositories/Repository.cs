using Microsoft.EntityFrameworkCore;
using ReminderBot.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }


        public IQueryable<TEntity> asQueryable(params string[] includes)
        {
            var query = _query(_context, includes);
            return query.AsQueryable();
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _query(_context, includes);

            return await query.Where(exp).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllPagenatedAsync(Expression<Func<TEntity, bool>> exp, int pageIndex, int pageSize, params string[] includes)
        {
            var query = _query(_context, includes);

            return await query.Where(exp).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _query(_context, includes);

            return await query.FirstOrDefaultAsync(exp);
        }

        public async Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _query(_context, includes);
            return await query.CountAsync(exp);

        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }


        public bool IsExist(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _query(_context, includes);

            return query.Any(exp);
        }
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _query(_context, includes);

            return await query.AnyAsync(exp);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        private IQueryable<TEntity> _query(DataContext context, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
