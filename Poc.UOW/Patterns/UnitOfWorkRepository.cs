using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Poc.UOW.Contexts;
using Poc.UOW.Repositories;
using System;

namespace Poc.UOW.Patterns
{
    public class UnitOfWorkRepository
    {
        private readonly ProjectDbContext context;
        private readonly ILoggerFactory loggerFactory;

        private readonly ILogger logger;


        private ProductRepository _productRep;
        public ProductRepository Products
        {
            get
            {
                _productRep = _productRep ?? 
                    new ProductRepository(context, loggerFactory.CreateLogger<ProductRepository>(), context.Database.CurrentTransaction.GetDbTransaction());
                return _productRep;
            }
        }



        private StarRatingRepository _starRatingRep;
        public StarRatingRepository StarRatings
        {
            get
            {
                _starRatingRep = _starRatingRep ??
                    new StarRatingRepository(context, loggerFactory.CreateLogger<StarRatingRepository>(), context.Database.CurrentTransaction.GetDbTransaction());
                return _starRatingRep;
            }
        }



        public UnitOfWorkRepository(ProjectDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.loggerFactory = loggerFactory;
            logger = this.loggerFactory.CreateLogger<UnitOfWorkRepository>();
            Begin();
        }

        public void Save()
        {
            logger.LogDebug("[ Called: Save ]");
            context.SaveChanges();
        }


        public void Begin()
        {
            logger.LogDebug("[ Called: Begin ]");

            ///Agora estou delegando a função de iniciar uma transação para o UOW;
            ///Como foram retirados todos os repositórios da Injeção de dependência,
            ///O repositório UOW é quem vai gerenciar a transação;
            if (context.Database != null && context.Database.CurrentTransaction is null)
            {
                context.Database.BeginTransaction();
            }

            _starRatingRep = null;
            _productRep = null;
        }

        public void Commit()
        {
            logger.LogDebug("[ Called: Commit ]");

            try
            {
                context.SaveChanges();
                context.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "[ Called: Commit ]");
                context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                ///É um exemplo somente para demonstrar como você poderia manter sempre uma transação compartilhada ativa;
                ///E para que você possa fazer Vários Commits dentro de uma única requisição, caso seja necessário.
                ///Não é a forma correta de se fazer isso, mas é um exemplo eficiente para este cenário simples!
                ///
                ///Sim, se for gerado uma exceção e cair no throw, ainda estarei iniciando uma conexão ao final da requisição.
                ///Mas perceba que estou somente provando que, após o commit, ainda é possível iniciar uma nova transação
                ///de uma forma centralizada, que pode te auxiliar em ocasiões específicas.
                Begin();
            }
        }

    }
}
