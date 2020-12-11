using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbContext DbContext => _dbContext;
        public DatabaseFacade Database => _dbContext.Database;
        public IDbConnection Connection => _dbContext.Database.GetDbConnection();

        public IDbContextTransaction BeginTransaction(IDbContextTransaction dbContextTransaction = null)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            if (dbContextTransaction != null)
            {
                return DbContextTransaction = dbContextTransaction;
            }

            return DbContextTransaction = Database.BeginTransaction();
        }

        public IDbContextTransaction DbContextTransaction { get; set; }

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default,
            params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Add<TEntity>(TEntity entity) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RangeAdd<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RangeUpdate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> RangeDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            if (DbContextTransaction != null)
                return await _dbContext.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> CommitAsync()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            if (DbContextTransaction == null)
            {
                var result = await _dbContext.SaveChangesAsync() > 0;
                Connection.Close();
                Connection.Dispose();
                return result;
            }

            await DbContextTransaction.CommitAsync();
            DbContextTransaction = null;
            Connection.Close();
            Connection.Dispose();
            return true;
        }

        public void Rollback()
        {
            DbContextTransaction?.Rollback();
        }
    }
}