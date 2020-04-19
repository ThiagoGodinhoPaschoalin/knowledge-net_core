using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Poc.UOWTransactionManagement.Patterns
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        int SaveChanges(bool saveAllChanges = true);
        Task<int> SaveChangesAsync(bool saveAllChanges = true);
        //void Begin();
        //void Commit();

        TModel Add(TModel model);
        Task<TModel> AddAsync(TModel model);

        void AddRange(IEnumerable<TModel> models);
        Task AddRangeAsync(IEnumerable<TModel> models);

        IEnumerable<TModel> GetAll();
        Task<IEnumerable<TModel>> GetAllAsync();

        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate);

        IEnumerable<TModel> GetNoTracking(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> GetNoTrackingAsync(Expression<Func<TModel, bool>> predicate);

        TModel GetOne(Guid modelId);
        Task<TModel> GetOneAsync(Guid modelId);

        TModel Remove(TModel model);

        TModel Update(TModel model);
    }

    public abstract class BaseRepository<TModel, TContext> : IBaseRepository<TModel>
        where TModel : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected readonly ILogger Logger;

        protected readonly IDbConnection Connection;

        protected IDbTransaction Transaction;

        private bool saveForEachCall = false;

        protected BaseRepository(TContext context, ILogger logger)
        {
            try
            {
                this.Context = context;
                this.Connection = this.Context.Database?.GetDbConnection();
                this.Transaction = this.Context.Database?.CurrentTransaction?.GetDbTransaction();
                this.Logger = logger;
                SetAtomicChanges(this.Transaction is null);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"[ BaseRepository.Constructor: {context.GetType().FullName} ]");
                throw;
            }
        }

        /// <summary>
        /// Forçar um SaveChanges para o repositório que não estiver participando de uma transação?
        /// </summary>
        /// <param name="saveAtomic"></param>
        protected void SetAtomicChanges(bool saveAtomic = false)
        {
            saveForEachCall = saveAtomic;
        }


        public int SaveChanges(bool saveAllChanges = true)
        {
            return Context.SaveChanges(saveAllChanges);
        }

        public async Task<int> SaveChangesAsync(bool saveAllChanges = true)
        {
            return await Context.SaveChangesAsync(saveAllChanges);
        }

        private void Begin()
        {
            try
            {
                if (Context.Database.CurrentTransaction is null)
                {
                    this.Transaction = Context.Database.BeginTransaction().GetDbTransaction();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ Begin: Exception ]");
                throw;
            }
        }

        private void Commit()
        {
            try
            {
                SaveChanges();

                if (Context.Database.CurrentTransaction != null)
                {
                    Context.Database.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                Context.Database.RollbackTransaction();
                Logger.LogError(ex, $"[ Commit: Exception ]");
                throw;
            }
        }



        #region Entity Framework

        public virtual TModel Add(TModel model)
        {
            try
            {
                var result = Context.Set<TModel>().Add(model);
                
                if (saveForEachCall)
                {
                    SaveChanges();
                }

                return result.Entity;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ Add: Exception ]");
                throw;
            }
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            try
            {
                var result = await Context.Set<TModel>().AddAsync(model);

                if (saveForEachCall)
                {
                    SaveChanges();
                }

                return result.Entity;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ AddAsync: Exception ]");
                throw;
            }
        }


        public virtual void AddRange(IEnumerable<TModel> models)
        {
            try
            {
                Context.Set<TModel>().AddRange(models);

                if (saveForEachCall)
                {
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ AddRange: Exception ]");
                throw;
            }
        }

        public virtual async Task AddRangeAsync(IEnumerable<TModel> models)
        {
            try
            {
                await Context.Set<TModel>().AddRangeAsync(models);

                if (saveForEachCall)
                {
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ AddRangeAsync: Exception ]");
                throw;
            }
        }


        public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return Context.Set<TModel>()?.Where(predicate).AsEnumerable();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ Get( with Expression ): Exception ]");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return await Context.Set<TModel>()?.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetAsync( with Expression ): Exception ]");
                throw;
            }
        }


        public virtual IEnumerable<TModel> GetAll()
        {
            try
            {
                return Context.Set<TModel>()?.AsEnumerable();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetAll: Exception ]");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            try
            {
                return await Context.Set<TModel>()?.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetAllAsync: Exception ]");
                throw;
            }
        }


        public virtual TModel GetOne(Guid modelId)
        {
            try
            {
                return Context.Set<TModel>().Find(modelId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetOne: Exception ]");
                throw;
            }
        }

        public virtual async Task<TModel> GetOneAsync(Guid modelId)
        {
            try
            {
                return await Context.Set<TModel>().FindAsync(modelId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetOneAsync: Exception ]");
                throw;
            }
        }


        public virtual IEnumerable<TModel> GetNoTracking(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return Context.Set<TModel>()?.AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ Get( with Expression ): Exception ]");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TModel>> GetNoTrackingAsync(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return await Context.Set<TModel>()?.AsNoTracking().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ GetAsync( with Expression ): Exception ]");
                throw;
            }
        }


        public virtual TModel Remove(TModel model)
        {
            try
            {
                //var existing = Context.Set<TModel>().Find(model);
                //if (existing != null)
                //{
                //    var result = Context.Set<TModel>().Remove(existing);
                //    return result.State == EntityState.Deleted;
                //}
                //return false;

                var result = Context.Set<TModel>().Attach(model);
                result.State = EntityState.Deleted;

                if (saveForEachCall)
                {
                    SaveChanges();
                }

                return result.Entity;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ Remove: Exception ]");
                throw;
            }
        }

        public virtual TModel Update(TModel model)
        {
            try
            {
                //var result = Context.Set<TModel>().Update(model);
                var result = Context.Set<TModel>().Attach(model);
                result.State = EntityState.Modified;

                if (saveForEachCall)
                {
                    SaveChanges();
                }

                return result.Entity;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[ Update: Exception ]");
                throw;
            }
        }

        #endregion


        #region Dapper

        protected TEntity QueryOne<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return this.Connection
                    .QueryFirstOrDefault<TEntity>(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ QueryOne ({typeof(TEntity).FullName}): Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }

        protected async Task<TEntity> QueryOneAsync<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return await this.Connection
                    .QueryFirstOrDefaultAsync<TEntity>(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ QueryOneAsync ({typeof(TEntity).FullName}): Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }


        protected IEnumerable<TEntity> QueryMany<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return this.Connection
                    .Query<TEntity>(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ QueryMany ({typeof(TEntity).FullName}): Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }

        protected async Task<IEnumerable<TEntity>> QueryManyAsync<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return await this.Connection
                    .QueryAsync<TEntity>(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ QueryManyAsync ({typeof(TEntity).FullName}): Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }


        protected int Execute(string sqlCommand, object parameters = null)
        {
            try
            {
                return this.Connection
                    .Execute(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ Execute: Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }

        protected async Task<int> ExecuteAsync(string sqlCommand, object parameters = null)
        {
            try
            {
                return await this.Connection
                    .ExecuteAsync(sqlCommand, parameters, this.Transaction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[ ExecuteAsync: Exception ]\n" +
                    $"[ Sql: {sqlCommand} ]\n" +
                    $"[ Parameters: {JsonSerializer.Serialize(parameters, options: new JsonSerializerOptions() { WriteIndented = true })} ]");
                throw;
            }
        }

        #endregion
    }
}
