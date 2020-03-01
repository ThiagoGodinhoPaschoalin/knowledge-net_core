using FreeHandApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FreeHandApi.Contexts
{
    public class FirstDbContext : DbContext
    {
        public FirstDbContext(DbContextOptions<FirstDbContext> options) 
            : base(options)
        { }

        public DbSet<FirstModel> Firsts { get; set; }
    }
}
