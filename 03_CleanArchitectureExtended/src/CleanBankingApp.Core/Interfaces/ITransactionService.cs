using CleanBankingApp.Core.Domain.Entities;
using System.Collections.Generic;

namespace CleanBankingApp.Core.Interfaces
{
    public interface ITransactionService
    {
        Transaction NewDeposit(Account account, decimal amount);
        Transaction NewWithdraw(Account account, decimal amount);
        Transaction NewTransfer(Account from, Account to, decimal amount);
        Transaction CreateTransaction(Transaction transaction);
        Transaction GetById(int id);
        List<Transaction> GetAll();
        int GetCount();
        bool Execute(Transaction transaction);
        bool Rollback(Transaction transaction);
    }
}