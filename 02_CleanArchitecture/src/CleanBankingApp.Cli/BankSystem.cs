using System;
using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp.Cli
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
            int option = ConsoleInput.ReadInteger("Choose an option", 1,
                Enum.GetNames(typeof(MenuOption)).Length);
            return (MenuOption)(option - 1);
        }

        static void CreateAccount(Bank bank)
        {
            string name = ConsoleInput.ReadString("Enter account name");
            decimal balance = ConsoleInput.ReadDecimal("Enter the opening balance");
            bank.AddAccount(new Account(name, balance));
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
                        string name = ConsoleInput.ReadString("Enter account name");
                        decimal balance = ConsoleInput.ReadDecimal("Enter the opening balance");
                        bank.AddAccount(new Account(name, balance)); 
                        break;

                    case MenuOption.Withdraw:
                        bank.DoWithdraw(); 
                        break;

                    case MenuOption.Deposit:
                        bank.DoDeposit(); 
                        break;

                    case MenuOption.Transfer:
                        bank.DoTransfer(); 
                        break;

                    case MenuOption.Rollback:
                        bank.DoRollback(); 
                        break;

                    case MenuOption.Print:
                        bank.DoPrint();
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
