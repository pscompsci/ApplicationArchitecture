using CleanBankingApp.Core.Domain.Entities;
using System.Collections.Generic;

namespace CleanBankingApp.Core.Interfaces
{
    public interface IAccountService
    {
        Account NewAccount(string name, decimal balance);
        Account CreateAccount(Account account);
        Account GetByName(string name);
        Account GetById(int id);
        List<Account> GetAll();
    }
}
