using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Poc.EFWithManyContexts.Modules.Occurrences.Contexts;
using Poc.EFWithManyContexts.Modules.Occurrences.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Poc.EFWithManyContexts.Patterns
{
    public class UnitOfWorkRepositories
    {
        private DbConnection dbConnection;
        private DbTransaction dbTransaction;
        private readonly TransactionDbContext transactionDbContext;

        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;

        public UnitOfWorkRepositories(TransactionDbContext transactionDbContext, ILoggerFactory loggerFactory)
        {
            this.transactionDbContext = transactionDbContext;
            this.loggerFactory = loggerFactory;

            logger = this.loggerFactory.CreateLogger<UnitOfWorkRepositories>();

            logger.LogInformation("[ UnitOfWorkRepositories Started! ]");

            ValidateTransaction();
        }



        #region Repositories

        private OccurrenceDbContext occurrenceDbContext;
        private OccurrenceRepository occurrenceRepository;
        public IOccurrenceRepository Occurrences
        {
            get
            {
                if (NeedChangeTransaction(nameof(Occurrences)))
                {
                    occurrenceDbContext = new OccurrenceDbContext(dbConnection);
                    occurrenceRepository = new OccurrenceRepository
                        (occurrenceDbContext, loggerFactory.CreateLogger<OccurrenceRepository>(), dbTransaction);
                }

                return occurrenceRepository;
            }
        }

        #endregion



        #region Handle

        /// <summary>
        /// Registra quem já está utilizando a conexão compartilhada!
        /// </summary>
        private Dictionary<string, bool> restartRepository;

        private bool NeedChangeTransaction(string repositoryName)
        {
            try
            {
                logger.LogInformation($"[ NeedChangeTransaction ] [ repositoryName: '{repositoryName}' ]");

                ValidateTransaction();

                if (restartRepository.ContainsKey(repositoryName))
                {
                    if (restartRepository[repositoryName])
                    {
                        restartRepository[repositoryName] = false;
                        return true;
                    }

                    return false;
                }
                else
                {
                    logger.LogWarning($"[ NeedChangeTransaction ] " +
                        $"[ O repositório '{repositoryName}' não foi encontrado no dicionário. ]");

                    throw new NullReferenceException($"O repositório '{repositoryName}' não foi encontrado no dicionário.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ NeedChangeTransaction ]");
                throw;
            }
        }

        private void ValidateTransaction()
        {
            try
            {
                logger.LogInformation("[ ValidateTransaction ]");

                if (transactionDbContext.Database.CurrentTransaction is null)
                {
                    logger.LogInformation("[ ValidateTransaction ] [ Begin a new transaction! ]");

                    transactionDbContext.Database.BeginTransaction();

                    dbConnection = transactionDbContext.Database.GetDbConnection();
                    dbTransaction = transactionDbContext.Database.CurrentTransaction.GetDbTransaction();

                    RegisterRepositories();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[ ValidateTransaction ]");
                throw;
            }
        }

        private void RegisterRepositories()
        {
            try
            {
                logger.LogInformation("[ RegisterRepositories ]");

                ///Recuperar os nomes de todos os repositórios que ficarão disponível para uso;
                var repositoriesNames = this.GetType().GetProperties()
                    .Where(x => x.PropertyType.Name.Contains("repository", StringComparison.InvariantCultureIgnoreCase))
                    .Distinct()
                    .Select(x => x.Name);

                ///Registrá-los. Gerenciar quando houver mudanças na conexão!
                restartRepository = repositoriesNames
                    .Select(name => KeyValuePair.Create(name, true))
                    .ToDictionary(k => k.Key, v => v.Value);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[ RegisterRepositories ]");
                throw;
            }
        }

        #endregion
    }
}