using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Services;
using CleanBankingApp.Infrastructure.Repositories;
using CleanBankingApp.Core.Domain.Entities;
using System;

namespace CleanBankingApp.Cli
{
    class Bank : IBank
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;

        public Bank()
        {
            _accounts = new AccountService(new InMemoryAccountRepository());
            _transactions = new TransactionService(new InMemoryTransactionRepository());
        }

        private string InputAccountName()
        {
            return ConsoleInput.ReadString("Enter the account name");
        }

        private decimal InputAmount()
        {
            return ConsoleInput.ReadDecimal("Enter the amount");
        }

        private Account FindAccount()
        {
            string name = InputAccountName();
            return _accounts.GetByName(name);
        }

        private void DoCreateAccount()
        {
            string name = InputAccountName();
            decimal balance = ConsoleInput.ReadDecimal("Enter the opening balance");
            _accounts.NewAccount(name, balance);
        }

        private void DoDeposit()
        {
            Account account = FindAccount();
            if (account is null) return;
            decimal amount = InputAmount();
            _transactions.NewDeposit(account, amount);
        }

        private void DoWithdraw()
        {
            Account account = FindAccount();
            if (account is null) return;

            decimal amount = InputAmount();
            _transactions.NewWithdraw(account, amount);
        }

        private void DoTransfer()
        {
            Console.WriteLine("Transfer from:");
            Account from = FindAccount();

            Console.WriteLine("Transfer to:");
            Account to = FindAccount();

            if (from is null || to is null) return;
            
            decimal amount = InputAmount();
            _transactions.NewTransfer(from, to, amount);
        }

        private void DoRollback()
        {
            PrintTransactionHistory();

            int result = ConsoleInput.ReadInteger(
                "Enter transaction # to rollback (0 for no rollback)",
                0, _transactions.GetCount());

            if (result == 0) return;
            _transactions.GetById(result).Rollback();
        }

        private void DoPrintAccount()
        {
            Account account = FindAccount();
            if (account is null) return;
            ConsoleOutput.DisplayAccount(account);
        }

        private void PrintTransactionHistory()
        {
            ConsoleOutput.DisplayTransactions(_transactions.GetAll());
        }

        private MenuOption ReadUserOption()
        {
            int option = ConsoleInput.ReadInteger("Choose an option", 1,
                Enum.GetNames(typeof(MenuOption)).Length);
            return (MenuOption)(option - 1);
        }

        public void InitData()
        {
            _accounts.NewAccount("Peter", 250);
            _accounts.NewAccount("Kay", 800);
        }

        public void StartUI()
        {
            do
            {
                ConsoleOutput.DisplayMenu();
                MenuOption chosen = ReadUserOption();

                switch (chosen)
                {
                    case MenuOption.CreateAccount:
                        DoCreateAccount(); 
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(); 
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(); 
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(); 
                        break;

                    case MenuOption.Rollback:
                        DoRollback(); 
                        break;

                    case MenuOption.Print:
                        DoPrintAccount();
                        break;

                    case MenuOption.History:
                        PrintTransactionHistory();
                        break;

                    case MenuOption.Quit:
                    default:
                        Console.WriteLine("Goodbye");
                        return;
                }
            } while (true);
        }
    }
}