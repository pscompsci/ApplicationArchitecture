using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System.Collections.Generic;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts;

        public InMemoryAccountRepository()
        {
            _accounts = new List<Account>();
        }

        public Account Create(Account account)
        {
            account.SetId(_accounts.Count + 1);
            _accounts.Add(account);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

        public Account GetByName(string name)
        {
            foreach (var account in _accounts)
                if (account.Name == name) return account;
            return null;
        }

        public Account GetById(int id)
        {
            foreach (var account in _accounts)
                if (account.Id == id) return account;
            return null;
        }

        public Account Update(Account account)
        {
            Account result = GetById(account.Id);
            if (result is null) return null;
            result = account;
            return result;
        }
    }
}
