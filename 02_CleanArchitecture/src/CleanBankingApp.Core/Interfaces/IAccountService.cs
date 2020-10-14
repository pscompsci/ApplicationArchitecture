using CleanBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanBankingApp.Core.Interfaces
{
    public interface IAccountService
    {
        Account NewAccount(string name, decimal balance);
        Account CreateAccount(Account account);
        Account GetByName(string name);
        Account GetById(int id);
        List<Account> GetAll();
        bool Deposit(Account account, decimal amount);
        bool Withdraw(Account account, decimal amount);
    }
}
