using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanBankingApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly AppDBContext _ctx;

        public TransactionRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        public Transaction Create(Transaction transaction)
        {
            Transaction trans = _ctx.Transactions.Add(transaction).Entity;
            _ctx.SaveChanges();
            return trans;

            // throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _ctx.Transactions;
        }

        public Transaction GetById(int id)
        {
            return _ctx.Transactions
                .FirstOrDefault(t => t.Id == id);
        }

        public Transaction GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Transaction Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
