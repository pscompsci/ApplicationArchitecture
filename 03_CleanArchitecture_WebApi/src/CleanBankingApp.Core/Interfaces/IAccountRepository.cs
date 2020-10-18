using CleanBankingApp.Core.Domain.Entities;
using System.Collections.Generic;

namespace CleanBankingApp.Core.Interfaces
{
    public interface IAccountRepository
    {
        Account Create(Account account);
        Account GetById(int id);
        Account GetByName(string name);
        IEnumerable<Account> GetAll();
        Account Update(Account account);
    }
}
