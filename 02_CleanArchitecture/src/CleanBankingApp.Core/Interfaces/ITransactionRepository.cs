using CleanBankingApp.Core.Domain.Entities;
using System.Collections.Generic;

namespace CleanBankingApp.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction Create(Transaction transaction);
        Transaction GetById(int id);
        Transaction GetByName(string name);
        IEnumerable<Transaction> GetAll();
        Transaction Update(Transaction transaction);
        int GetCount();
    }
}