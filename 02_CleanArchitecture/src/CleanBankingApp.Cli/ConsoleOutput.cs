using System;
using System.Collections.Generic;
using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp.Cli
{
    public class ConsoleOutput
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("\n********************");
            Console.WriteLine("*      Menu        *");
            Console.WriteLine("********************");
            Console.WriteLine("* 1. New Account   *");
            Console.WriteLine("* 2. Withdraw      *");
            Console.WriteLine("* 3. Deposit       *");
            Console.WriteLine("* 4. Transfer      *");
            Console.WriteLine("* 5. Rollback      *");
            Console.WriteLine("* 6. Print Account *");
            Console.WriteLine("* 7. Print History *");
            Console.WriteLine("* 8. Quit          *");
            Console.WriteLine("********************");
        }

        public static void DisplayAccount(Account account)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("| {0,-15} | {1,10} |", "Name", "Balance");
            Console.WriteLine("================================");
            Console.WriteLine("| {0,-15} | {1,10} |", account.Name, account.Balance.ToString("C"));
            Console.WriteLine("================================");
        }

        public static void DisplayTransactions(List<Transaction> transactions)
        {
            Console.WriteLine(new String('-', 85));
            Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", "#",
                    "DateTime", "Type", "Amount", "Status");
            Console.WriteLine(new String('=', 85));
            foreach (var transaction in transactions)
            {
                Console.WriteLine("| {0,2} |{1,-25} | {2,-15}|{3,15} | {4,15} |", 
                    transaction.Id, transaction.DateStamp, transaction.Type,
                    transaction.Amount.ToString("C"), transaction.Status);
            }
            Console.WriteLine(new String('=', 85));
        }
    }
}