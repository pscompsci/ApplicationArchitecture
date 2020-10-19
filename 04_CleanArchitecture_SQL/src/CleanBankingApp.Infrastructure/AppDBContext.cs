using CleanBankingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanBankingApp.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
