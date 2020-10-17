using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanBankingApp.Core.Constants;
using System.IO;

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
            if (!IsValidName(name))
                throw new InvalidDataException(Messages.InvalidAccountName);

            if (!IsValidAmount(balance))
                throw new NegativeAmountException(Messages.NegativeBalance);

            Account account = new Account(name, balance);
            
            return CreateAccount(account);
        }

        public Account CreateAccount(Account account)
        {
            if (!IsValidName(account.Name))
                throw new InvalidDataException(Messages.InvalidAccountName);

            if (!IsValidAmount(account.Balance))
                throw new NegativeAmountException(Messages.NegativeBalance);

            return _accountRepository.Create(account);
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetAll().ToList();
        }

        public Account GetById(int id)
        {
            Account account = _accountRepository.GetById(id);

            if (account is null)
                throw new NullReferenceException($"Account with Id: {id}, not found");
            
            return account;
        }

        public Account GetByName(string name)
        {
            if (!IsValidName(name))
                throw new InvalidDataException(Messages.InvalidAccountName);

            Account account = _accountRepository.GetByName(name);

            if (account is null)
                throw new NullReferenceException($"Account with Name: {name}, not found");

            return account;
        }

        private bool IsValidName(string name) => !String.IsNullOrEmpty(name);

        private bool IsValidAmount(decimal amount) => amount > 0;
    }
}
