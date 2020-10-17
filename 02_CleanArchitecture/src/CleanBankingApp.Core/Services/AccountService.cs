using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            Account account = new Account(name, balance);
            return CreateAccount(account);
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepository.Create(account);
        }

        public bool Deposit(Account account, decimal amount)
        {
            bool success = account.Deposit(amount);
            if (!success) return false;
            _accountRepository.Update(account);
            return true;
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetAll().ToList();
        }

        public Account GetById(int id)
        {
            return _accountRepository.GetById(id);
        }

        public Account GetByName(string name)
        {
            return _accountRepository.GetByName(name);
        }

        public bool Withdraw(Account account, decimal amount)
        {
            bool success = account.Withdraw(amount);
            if (!success) return false;
            _accountRepository.Update(account);
            return true;
        }
    }
}
