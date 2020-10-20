using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using CleanBankingApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CleanBankingApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account NewAccount(string name, decimal balance)
        {
            Guard.Against.NullOrEmpty(name, "Name");
            Guard.Against.Negative(balance, "Balance");

            return CreateAccount(new Account(name, balance));
        }

        public Account CreateAccount(Account account)
        {
            Guard.Against.NullOrEmpty(account.Name, "Name");
            Guard.Against.Negative(account.Balance, "Balance");

            return _accountRepository.Create(account);
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetAll().ToList();
        }

        public Account GetById(int id)
        {
            Account account = _accountRepository.GetById(id);

            if (account is null) throw new NullReferenceException();
            return account;
        }

        public Account GetByName(string name)
        {
            Guard.Against.NullOrEmpty(name, "Name");

            Account account = _accountRepository.GetByName(name);
            Guard.Against.Null(account, "Account"); ;

            return account;
        }
    }
}
