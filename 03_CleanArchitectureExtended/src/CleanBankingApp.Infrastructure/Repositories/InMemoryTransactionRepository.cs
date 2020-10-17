using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System.Collections.Generic;
using System;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        public InMemoryTransactionRepository()
        {
        }

        public Transaction Create(Transaction transaction)
        {
            transaction.SetId(FakeDB.Transactions.Count + 1);
            FakeDB.Transactions.Add(transaction);
            return transaction;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return FakeDB.Transactions;
        }

        public Transaction GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Transaction GetById(int id)
        {
            foreach (var transaction in FakeDB.Transactions)
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

        public int GetCount()
        {
            return FakeDB.Transactions.Count;
        }
    }
}
