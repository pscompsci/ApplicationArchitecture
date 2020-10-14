using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Services;
using CleanBankingApp.Infrastructure.Repositories;
using CleanBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CleanBankingApp
{
    class Bank
    {
        private readonly IAccountService _accounts;
        public List<Transaction> Transactions { get; private set; }

        public Bank()
        {
            _accounts = new AccountService(new InMemoryAccountRepository());
            Transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            _accounts.CreateAccount(account);
        }

        public Account GetAccount(string name)
        {
            return _accounts.GetByName(name);
        }

        public void Execute(Transaction transaction)
        {
            Transactions.Add(transaction);
            try
            {
                transaction.Execute();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("An error occurred in executing the transaction");
                Console.WriteLine("The error was: " + exception.Message);
            }
        }

        public void Rollback(Transaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("An error occurred in rolling the transaction back");
                Console.WriteLine("The error was: " + exception.Message);
            }
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine(new String('-', 85));
            Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", "#",
                    "DateTime", "Type", "Amount", "Status");
            Console.WriteLine(new String('=', 85));
            for (int i = 0; i < Transactions.Count; i++)
            {
                Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", i + 1,
                    Transactions[i].DateStamp, Transactions[i].Type,
                    Transactions[i].Amount.ToString("C"), Transactions[i].Status);
            }
            Console.WriteLine(new String('=', 85));
        }
    }
}