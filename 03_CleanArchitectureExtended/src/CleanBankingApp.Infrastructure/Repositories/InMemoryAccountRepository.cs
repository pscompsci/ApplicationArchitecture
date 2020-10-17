using CleanBankingApp.Infrastructure.Exceptions;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System.Collections.Generic;
using System;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        public InMemoryAccountRepository()
        {
            if (FakeDB.Accounts.Count >= 1) return;
            InitData();
        }

        public Account Create(Account account)
        {
            account.SetId(FakeDB.Accounts.Count + 1);
            FakeDB.Accounts.Add(account);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return FakeDB.Accounts;
        }

        public Account GetByName(string name)
        {
            foreach (var account in FakeDB.Accounts)
                if (account.Name == name) return account;
            throw new AccountDoesNotExistException($"Account with Name: {name}, does not exist.");
        }

        public Account GetById(int id)
        {
            foreach (var account in FakeDB.Accounts)
                if (account.Id == id) return account;
            throw new AccountDoesNotExistException($"Account with Id: {id}, does not exist.");
        }

        public Account Update(Account account)
        {
            Account result = GetById(account.Id);
            if (result is null) return null;
            result = account;
            return result;
        }

        private void InitData()
        {
            Create(new Account("Peter", 250));
            Create(new Account("Kay", 800));
            Create(new Account("Oliver", 500));
        }
    }
}
