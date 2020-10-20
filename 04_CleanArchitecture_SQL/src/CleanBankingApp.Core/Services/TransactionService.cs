using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Enums;
using CleanBankingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Guard.Against.Null(account, "Account");
            Guard.Against.Negative(amount, "Amount");

            DepositTransaction deposit = new DepositTransaction(account, amount);

            CreateTransaction(deposit);
            Execute(deposit);
            
            return deposit;
        }

        public Transaction NewTransfer(Account from, Account to, decimal amount)
        {
            Guard.Against.Null(from, "From");
            Guard.Against.Null(to, "To");
            Guard.Against.Negative(amount, "Amount");

            TransferTransaction transfer = new TransferTransaction(from, to, amount);

            CreateTransaction(transfer);
            Execute(transfer);

            return transfer;
        }

        public Transaction NewWithdraw(Account account, decimal amount)
        {
            Guard.Against.Null(account, "Account");
            Guard.Against.Negative(amount, "Amount");
                
            WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);

            CreateTransaction(withdraw);
            Execute(withdraw);

            return withdraw;
        }

        private Transaction CreateTransaction(Transaction transaction)
        {
            return _transactionRepository.Create(transaction);
        }

        private bool Execute(Transaction transaction)
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
            Guard.Against.Null(transaction, "Transaction");

            switch (transaction.Type)
            {
                case "Deposit":
                    transaction = transaction as DepositTransaction;
                    break;
                case "Withdraw":
                    transaction = transaction as WithdrawTransaction;
                    break;
                case "Transfer":
                    transaction = transaction as TransferTransaction;
                    break;
                default:
                    return null;
            }

            transaction.Rollback();
            return _transactionRepository.Update(transaction); 
        }
    }
}