using Microsoft.EntityFrameworkCore;

namespace Poc.EFWithManyContexts.Patterns
{
    /// <summary>
    /// Ele estará no Startup, carregando o Pool de conexão para o contexto;
    /// </summary>
    public sealed class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions options) 
            : base(options)
        { }
    }
}