using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanBankingApp.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Transaction NewDeposit(Account account, decimal amount)
        {
            DepositTransaction deposit = new DepositTransaction(account, amount);
            CreateTransaction(deposit);
            Execute(deposit);
            _transactionRepository.Update(deposit);
            return deposit;
        }

        public Transaction NewTransfer(Account from, Account to, decimal amount)
        {
            TransferTransaction transfer = new TransferTransaction(from, to, amount);
            CreateTransaction(transfer);
            Execute(transfer);
            _transactionRepository.Update(transfer);
            return transfer;
        }

        public Transaction NewWithdraw(Account account, decimal amount)
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);
            CreateTransaction(withdraw);
            Execute(withdraw);
            _transactionRepository.Update(withdraw);
            return withdraw;
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            return _transactionRepository.Create(transaction);
        }

        public bool Execute(Transaction transaction)
        {
            try
            {
                transaction.Execute();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Transaction> GetAll()
        {
            return _transactionRepository.GetAll().ToList();
        }

        public Transaction GetById(int id)
        {
            return _transactionRepository.GetById(id);
        }

        public int GetCount()
        {
            return _transactionRepository.GetCount();
        }

        public Transaction Rollback(Transaction transaction)
        {
            _ = transaction.Rollback();
            return _transactionRepository.Update(transaction);
        }
    }
}