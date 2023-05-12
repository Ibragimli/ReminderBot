using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderBot.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        IQueryable<TEntity> asQueryable(params string[] includes);
        Task<IEnumerable<TEntity>> GetAllPagenatedAsync(Expression<Func<TEntity, bool>> exp, int pageIndex, int pageSize, params string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        bool IsExist(Expression<Func<TEntity, bool>> exp, params string[] includes);
        void Remove(TEntity entity);
    }
}
