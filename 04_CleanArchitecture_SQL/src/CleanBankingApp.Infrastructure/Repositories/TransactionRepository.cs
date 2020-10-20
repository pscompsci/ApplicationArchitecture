using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            trans.Execute();
            _ctx.SaveChanges();
            return trans;
        }

        public IEnumerable<Transaction> GetAll()
        {
            System.Console.WriteLine("Here");
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
            return _ctx.Transactions.Count();
        }

        public Transaction Update(Transaction transaction)
        {
            _ctx.Update(transaction);
            _ctx.SaveChanges();
            return transaction;
        }
    }
}
