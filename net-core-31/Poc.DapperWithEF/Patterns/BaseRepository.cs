using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Poc.DapperWithEF.Patterns
{
    /// <summary>
    /// Qualquer repositório criado, deve herdar o padrão abstraindo as funcionalidades bases; 
    /// Abstract: obrigar a classe ser herdada para ser utilizada; 
    /// 
    /// </summary>
    /// <remarks>
    /// 
    ///     .GetDbConnection() e .GetDbTransaction():
    ///         São métodos de extenção do Assembly 'Microsoft.EntityFrameworkCore.Storage',
    ///         que está contido em 'Microsoft.EntityFrameworkCore.Relational'.
    ///         
    ///     Pq não utilizar async/await?:
    ///         Isso vai depender muito do contexto, mas lembre-se de que a cada assinatura com async, 
    ///         é gerado uma máquina de estado no IL e se não usado com sabedoria, poderá afetar a performance do serviço;
    /// 
    ///     Pq as assinaturas dos métodos Dapper são 'protected'?
    ///         Repare que o contrato de saída não é o modelo padrão utilizado pela classe derivada.
    ///         O que significa que, com o Dapper, você terá mais liberdade em suas consultas e retornos;
    ///         Atenção: Mais liberdade, é mais poder, o que acarreta mais responsabilidade!
    /// 
    /// 
    /// </remarks>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        /// <summary>
        /// O seu contexto já deve herdar DbContext para que seja gerenciada pelo EF;
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// Contrato padrão;
        /// </summary>
        /// <param name="context"></param>
        protected BaseRepository(DbContext context)
        {
            this.context = context;

            ///Sempre iniciar uma transação no ciclo de vida da requisição
            ///Importância de sempre iniciar a transição:
            ///     Com isso, eu posso facilmente compartilhar essa transição com o Dapper, os mantendo no mesmo escopo!
            if (this.context.Database?.CurrentTransaction is null)
            {
                this.context.Database.BeginTransaction();
            }
        }



        /// <summary>
        /// Para que a classe derivada possa gerir sua própria regra de negócio em seu repositório!
        /// </summary>
        protected DbSet<TModel> GetModel => context.Set<TModel>();

        /// <summary>
        /// Para que a classe derivada possa gerir sua própria regra de negócio em seu repositório!
        /// </summary>
        /// <param name="model">Modelo que será manipulado pelo 'EntityEntry'</param>
        /// <returns></returns>
        protected EntityEntry<TModel> GetEntry(TModel model) => context.Entry(model);

        /// <summary>
        /// Perceba que o Dapper possui seus métodos baseados na extensão do DbConnection; 
        /// A partir do momento que eu utilizo a conexão do EF para ser utilizado no Dapper, eu unifico o escopo da conexão entre EF e Dapper; 
        /// Regra: SEMPRE usar 'context.Database.CurrentTransaction.GetDbTransaction()' em IDbTransaction da consulta Dapper;
        /// </summary>
        protected IDbConnection GetDapperModel => context.Database.GetDbConnection();



        #region Entity Framework

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TModel Add(TModel model)
        {
            try
            {
                var result = context.Set<TModel>().Add(model);
                //context.SaveChanges();
                return result.Entity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return context.Set<TModel>()?.Where(predicate).AsEnumerable();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TModel> GetAll()
        {
            try
            {
                return context.Set<TModel>()?.AsEnumerable();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public virtual TModel GetOne(Guid modelId)
        {
            try
            {
                return context.Set<TModel>().Find(modelId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Remove(TModel model)
        {
            try
            {
                var existing = context.Set<TModel>().Find(model);
                if (existing != null)
                {
                    var result = context.Set<TModel>().Remove(existing);
                    return result.State == EntityState.Deleted;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TModel Update(TModel model)
        {
            try
            {
                context.Entry(model).State = EntityState.Modified;
                var result = context.Set<TModel>().Attach(model);
                return result.Entity;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Dapper

        protected TEntity QueryOne<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return context.Database
                    .GetDbConnection()
                    .QueryFirstOrDefault<TEntity>(sqlCommand, parameters, context.Database.CurrentTransaction.GetDbTransaction());
            }
            catch
            {
                throw;
            }
        }

        protected IEnumerable<TEntity> QueryMany<TEntity>(string sqlCommand, object parameters = null)
        {
            try
            {
                return context.Database
                    .GetDbConnection()
                    .Query<TEntity>(sqlCommand, parameters, context.Database.CurrentTransaction.GetDbTransaction());
            }
            catch
            {
                throw;
            }
        }

        protected int Execute(string sqlCommand, object parameters = null)
        {
            try
            {
                return context.Database
                    .GetDbConnection().Execute(sqlCommand, parameters, context.Database.CurrentTransaction.GetDbTransaction());
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }


    /// <summary>
    /// Na sua interface de repositório derivado, 
    /// faça a herança desta interface para que os serviços que utilizem seus repositórios derivados, 
    /// possam tbm utilizar estas assinaturas!
    /// 
    /// A não ser que, faça sentido para você proteger estas classes das camadas externas!
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IBaseRepository<TModel>
    {
        TModel Add(TModel model);
        bool Remove(TModel model);
        TModel Update(TModel model);
        TModel GetOne(Guid modelId);
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> predicate);
        IEnumerable<TModel> GetAll();
    }
}