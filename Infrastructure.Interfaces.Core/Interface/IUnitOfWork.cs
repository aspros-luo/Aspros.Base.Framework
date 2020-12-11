using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces.Core.Interface
{
    public interface IUnitOfWork
    {
        IDbContext DbContext { get; }
        
        DatabaseFacade Database { get; }

        IDbConnection Connection { get; }

        //        IDbTransaction Transaction { get; }
        IDbContextTransaction BeginTransaction(IDbContextTransaction dbContextTransaction = null);
        IDbContextTransaction DbContextTransaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default,
            params object[] parameters);

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<bool> RangeAdd<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<bool> RangeUpdate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> RangeDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 提交 save change
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}