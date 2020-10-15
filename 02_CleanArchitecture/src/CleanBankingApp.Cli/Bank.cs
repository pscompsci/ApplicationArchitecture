using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Services;
using CleanBankingApp.Infrastructure.Repositories;
using CleanBankingApp.Core.Domain.Entities;
using System;

namespace CleanBankingApp.Cli
{
    class Bank
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;

        public Bank()
        {
            _accounts = new AccountService(new InMemoryAccountRepository());
            _transactions = new TransactionService(new InMemoryTransactionRepository());
        }

        public void AddAccount(Account account)
        {
            _accounts.CreateAccount(account);
        }

        public Account GetAccount(string name)
        {
            return _accounts.GetByName(name);
        }

        private Account FindAccount()
        {
            string name = ConsoleInput.ReadString("Enter the account name");
            return _accounts.GetByName(name);
        }

        public void DoDeposit()
        {
            Account account = FindAccount();
            if (account != null)
            {
                decimal amount = ConsoleInput.ReadDecimal("Enter the amount");
                _transactions.NewDeposit(account, amount);
            }
        }

        public void DoWithdraw()
        {
            Account account = FindAccount();
            if (account != null)
            {
                decimal amount = ConsoleInput.ReadDecimal("Enter the amount");
                _transactions.NewWithdraw(account, amount);
            }
        }

        public void DoTransfer()
        {
            Console.WriteLine("Transfer from:");
            Console.WriteLine("Transfer to:");
            Account from = FindAccount();
            Account to = FindAccount();
            if (from != null && to != null)
            {
                decimal amount = ConsoleInput.ReadDecimal("Enter the amount");
                _transactions.NewTransfer(from, to, amount);
            }
        }

        public void DoRollback()
        {
            PrintTransactionHistory();
            int result = ConsoleInput.ReadInteger(
                "Enter transaction # to rollback (0 for no rollback)",
                0, _transactions.GetCount());

            if (result == 0)
                return;

            Rollback(_transactions.GetById(result));
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

        public void DoPrint()
        {
            Account account = FindAccount();
            if (account != null)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("| {0,-15} | {1,10} |", "Name", "Balance");
                Console.WriteLine("================================");
                Console.WriteLine("| {0,-15} | {1,10} |", account.Name, account.Balance.ToString("C"));
                Console.WriteLine("================================");
            }
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine(new String('-', 85));
            Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", "#",
                    "DateTime", "Type", "Amount", "Status");
            Console.WriteLine(new String('=', 85));
            foreach (var transaction in _transactions.GetAll())
            {
                Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", 
                    transaction.Id, transaction.DateStamp, transaction.Type,
                    transaction.Amount.ToString("C"), transaction.Status);
            }
            Console.WriteLine(new String('=', 85));
        }
    }
}