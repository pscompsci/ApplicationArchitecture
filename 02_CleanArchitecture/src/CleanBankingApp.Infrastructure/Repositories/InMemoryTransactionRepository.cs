using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System.Collections.Generic;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions;

        public InMemoryTransactionRepository()
        {
            _transactions = new List<Transaction>();
        }

        public Transaction Create(Transaction transaction)
        {
            transaction.SetId(_transactions.Count + 1);
            _transactions.Add(transaction);
            return transaction;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactions;
        }

        public Transaction GetByName(string name)
        {
            return null;
        }

        public Transaction GetById(int id)
        {
            foreach (var transaction in _transactions)
                if (transaction.Id == id) return transaction;
            return null;
        }

        public Transaction Update(Transaction transaction)
        {
            Transaction result = GetById(transaction.Id);
            if (result is null) return null;
            result = transaction;
            return result;
        }
    }
}
