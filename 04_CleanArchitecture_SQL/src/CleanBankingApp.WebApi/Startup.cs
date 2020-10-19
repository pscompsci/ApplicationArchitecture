using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Services;
using CleanBankingApp.Infrastructure.Repositories;
using CleanBankingApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CleanBankingApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(opt => opt.UseInMemoryDatabase("BankingDB"));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionsManager, TransactionsManager>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using var scope = app.ApplicationServices.CreateScope();
                var ctx = scope.ServiceProvider.GetService<AppDBContext>();
                ctx.Accounts.Add(new Core.Domain.Entities.Account()
                {
                    Name = "Peter",
                    Balance = 500
                });
                ctx.Accounts.Add(new Core.Domain.Entities.Account()
                {
                    Name = "Kay",
                    Balance = 600
                });
                ctx.SaveChanges();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
