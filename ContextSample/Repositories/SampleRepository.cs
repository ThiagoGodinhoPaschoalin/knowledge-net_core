using ContextSample.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ContextSample.Repositories
{
    public class SampleRepository
    {
        private readonly SampleDbContext context;
        private readonly ILoggerFactory loggerFactory;

        public SampleRepository(SampleDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.context.Database.BeginTransaction();

            this.loggerFactory = loggerFactory;
        }

        public SampleProductRepository Products
        {
            get
            {
                var logger = loggerFactory.CreateLogger<SampleProductRepository>();
                var transaction = context.Database.CurrentTransaction.GetDbTransaction();
                return new SampleProductRepository(context, logger, transaction);
            }
        }

        public SampleStarRatingRepository StarRating
        {
            get
            {
                var logger = loggerFactory.CreateLogger<SampleStarRatingRepository>();
                var transaction = context.Database.CurrentTransaction.GetDbTransaction();
                return new SampleStarRatingRepository(context, logger, transaction);
            }
        }



        public void Commit()
        {
            try
            {
                context.SaveChanges();
                context.Database.CommitTransaction();
            }
            catch
            {
                context.Database.RollbackTransaction();
                throw; 
            }
            finally
            {
                context.Database.BeginTransaction();
            }
        }
    }
}
