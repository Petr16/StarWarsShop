using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using StarWarsShop.DAL.Repositories;

namespace StarWarsShop.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        //private RussianSpaceDatabase _db;

        public GenericRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; }
        /*
        /// <summary>
        /// API для работы с БД CustomerManagerDB
        /// </summary>
        protected CustomerManagerDatabase Database
        {
            get
            {
                if (_db == null)
                {
                    var connection = GetDbConnection();
                    _db = new CustomerManagerDatabase(connection);
                }
                return _db;
            }
        }*/

        private DbConnection GetDbConnection()
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection;
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return DbSet.CountAsync(cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DbSet.CountAsync(predicate, cancellationToken);
        }


        #region Get

        public IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            return asNoTracking ? DbSet.AsNoTracking() : DbSet;
        }

        public IAsyncEnumerable<TEntity> GetAllAsyncEnumerable(bool asNoTracking = true)
        {
            IQueryable<TEntity> entities = asNoTracking ? DbSet.AsNoTracking() : DbSet;
            return entities.AsAsyncEnumerable();
        }

        public async Task<List<TEntity>> GetAllToListAsync(CancellationToken cancellationToken = default, bool asNoTracking = true)
        {
            IQueryable<TEntity> entities = asNoTracking ? DbSet.AsNoTracking() : DbSet;
            return await entities.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public ValueTask<TEntity> GetByIdAsync(params object[] keyValues)
        {
            return DbSet.FindAsync(keyValues);
        }

        public ValueTask<TEntity> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return DbSet.FindAsync(keyValues, cancellationToken);
        }

        #endregion


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            DbSet.Remove(entity);
        }

        public async Task<bool> RemoveByIdAsync(params object[] keyValues)
        {
            TEntity entityToRemove = await DbSet.FindAsync(keyValues);
            if (entityToRemove == null)
                return false;

            DbSet.Remove(entityToRemove);
            return true;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            IEnumerable<TEntity> entityList = entities as IList<TEntity> ?? entities.ToList();
            foreach (var entity in entityList.Where(e => _context.Entry(e).State == EntityState.Detached))
                DbSet.Attach(entity);

            DbSet.RemoveRange(entityList);
        }
    }
}
