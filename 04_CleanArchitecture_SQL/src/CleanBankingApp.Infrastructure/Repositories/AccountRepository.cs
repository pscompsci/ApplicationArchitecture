using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDBContext _ctx;

        public AccountRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        public Account Create(Account account)
        {
            Account acc = _ctx.Accounts.Add(account).Entity;
            _ctx.SaveChanges();
            return acc;
        }

        public IEnumerable<Account> GetAll()
        {
            return _ctx.Accounts;
        }

        public Account GetById(int id)
        {
            return _ctx.Accounts
                .FirstOrDefault(a => a.Id == id);
        }

        public Account GetByName(string name)
        {
            return _ctx.Accounts
                .FirstOrDefault(a => a.Name == name);
        }

        public Account Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
