using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsShop.DAL.Repositories//StarWarsShop.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Asynchronously returns the number of elements in a sequence
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete</param>
        /// <returns></returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence that satisfy a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


        #region Get

        IQueryable<TEntity> GetAll(bool asNoTracking = true);

        IAsyncEnumerable<TEntity> GetAllAsyncEnumerable(bool asNoTracking = true);

        Task<List<TEntity>> GetAllToListAsync(CancellationToken cancellationToken = default, bool asNoTracking = true);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns></returns>
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        ValueTask<TEntity> GetByIdAsync(params object[] keyValues);

        ValueTask<TEntity> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken = default);

        #endregion


        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);

        Task<bool> RemoveByIdAsync(params object[] keyValues);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

