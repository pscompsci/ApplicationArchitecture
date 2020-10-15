using CleanBankingApp.Core.Interfaces;
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
            services.AddScoped<ITransactionRepository, InMemoryTransactionRepository>();
            services.AddScoped<IBank, Bank>();

            var provider = services.BuildServiceProvider();
            var bank = provider.GetRequiredService<IBank>();

            bank.InitData();
            bank.StartUI();
        }
    }
}
