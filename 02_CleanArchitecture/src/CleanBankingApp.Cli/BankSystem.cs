using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Services;
using CleanBankingApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanBankingApp.Cli
{
    class BankSystem
    {        
        public static void Main()
        {
            var services = new ServiceCollection();
            services.AddScoped<IAccountRepository, InMemoryAccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ITransactionRepository, InMemoryTransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IBank, Bank>();

            var provider = services.BuildServiceProvider();
            var bank = provider.GetRequiredService<IBank>();

            bank.InitData();
            bank.StartUI();
        }
    }
}
