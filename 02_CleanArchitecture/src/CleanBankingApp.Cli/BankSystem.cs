using System;
using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp
{
    enum MenuOption
    {
        CreateAccount,
        Withdraw,
        Deposit,
        Transfer,
        Rollback,
        Print,
        Quit
    }

    class BankSystem
    {
        public static string ReadString(string prompt)
        {
            Console.Write(prompt + ": ");
            return Console.ReadLine();
        }

        public static int ReadInteger(string prompt)
        {
            string numberInput = ReadString(prompt);
            while (!(int.TryParse(numberInput, out _)))
            {
                Console.WriteLine("Please enter a whole number");
                numberInput = ReadString(prompt);
            }
            return Convert.ToInt32(numberInput);
        }

        public static int ReadInteger(string prompt, int minimum, int maximum)
        {
            int number = ReadInteger(prompt);
            while (number < minimum || number > maximum)
            {
                Console.WriteLine("Please enter a whole number from " +
                                  minimum + " to " + maximum);
                number = ReadInteger(prompt);
            }
            return number;
        }

        public static decimal ReadDecimal(string prompt)
        {
            string numberInput = ReadString(prompt);
            while (!(decimal.TryParse(numberInput, out decimal number)) || number < 0)
            {
                Console.WriteLine("Please enter a decimal number, $0.00 or greater");
                numberInput = ReadString(prompt);
            }
            return Convert.ToDecimal(numberInput);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n********************");
            Console.WriteLine("*       Menu       *");
            Console.WriteLine("********************");
            Console.WriteLine("*  1. New Account  *");
            Console.WriteLine("*  2. Withdraw     *");
            Console.WriteLine("*  3. Deposit      *");
            Console.WriteLine("*  4. Transfer     *");
            Console.WriteLine("*  5. Rollback     *");
            Console.WriteLine("*  6. Print        *");
            Console.WriteLine("*  7. Quit         *");
            Console.WriteLine("********************");
        }

        private static MenuOption ReadUserOption()
        {
            DisplayMenu();
            int option = ReadInteger("Choose an option", 1,
                Enum.GetNames(typeof(MenuOption)).Length);
            return (MenuOption)(option - 1);
        }

        static void DoDeposit(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                decimal amount = ReadDecimal("Enter the amount");
                DepositTransaction deposit = new DepositTransaction(account, amount);
                try
                {
                    bank.Execute(deposit);
                }
                catch (InvalidOperationException)
                {
                    deposit.Print();
                    return;
                }
                deposit.Print();
            }
        }

        static void DoWithdraw(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                decimal amount = ReadDecimal("Enter the amount");
                WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);
                try
                {
                    bank.Execute(withdraw);
                }
                catch (InvalidOperationException)
                {
                    withdraw.Print();
                    return;
                }
                withdraw.Print();
            }
        }

        static void DoTransfer(Bank bank)
        {
            Console.WriteLine("Transfer from:");
            Account from = FindAccount(bank);
            Console.WriteLine("Transfer to:");
            Account to = FindAccount(bank);
            if (from != null && to != null)
            {
                decimal amount = ReadDecimal("Enter the amount");
                TransferTransaction transfer = new TransferTransaction(from, to, amount);
                bank.Execute(transfer);
                transfer.Print();
            }
        }

        static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("| {0,-15} | {1,10} |", "Name", "Balance");
                Console.WriteLine("================================");
                Console.WriteLine("| {0,-15} | {1,10} |", account.Name, account.Balance.ToString("C"));
                Console.WriteLine("================================");
            }
        }

        static void DoRollback(Bank bank)
        {
            bank.PrintTransactionHistory();
            int result = ReadInteger(
                "Enter transaction # to rollback (0 for no rollback)",
                0, bank.Transactions.Count);

            if (result == 0)
                return;

            bank.Rollback(bank.Transactions[result - 1]);
        }

        static void CreateAccount(Bank bank)
        {
            string name = ReadString("Enter account name");
            decimal balance = ReadDecimal("Enter the opening balance");
            bank.AddAccount(new Account(name, balance));
        }

        private static Account FindAccount(Bank bank)
        {
            string name = ReadString("Enter the account name");
            Account account = bank.GetAccount(name);
            if (account == null)
            {
                Console.WriteLine("That account name does not exist at this bank");
            }
            return account;
        }

        public static void Main()
        {
            Bank bank = new Bank();

            Account peter = new Account("Peter", 250);
            bank.AddAccount(peter);

            Account kay = new Account("Kay", 800);
            bank.AddAccount(kay);

            do
            {
                MenuOption chosen = ReadUserOption();
                switch (chosen)
                {
                    case MenuOption.CreateAccount:
                        CreateAccount(bank); break;

                    case MenuOption.Withdraw:
                        DoWithdraw(bank); break;

                    case MenuOption.Deposit:
                        DoDeposit(bank); break;

                    case MenuOption.Transfer:
                        DoTransfer(bank); break;

                    case MenuOption.Rollback:
                        DoRollback(bank); break;

                    case MenuOption.Print:
                        DoPrint(bank); break;

                    case MenuOption.Quit:
                    default:
                        Console.WriteLine("Goodbye");
                        return;
                }
            } while (true);
        }
    }
}
