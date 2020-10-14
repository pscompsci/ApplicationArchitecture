using System;
using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp
{
    class DepositTransaction : Transaction
    {
        public Account Account { get; }

        public DepositTransaction(Account account, decimal amount) : base(amount, "Deposit")
        {
            Account = account;
        }

        public override void Print()
        {
            Console.WriteLine(new String('-', 85));
            Console.WriteLine("|{0, -20}|{1, 20}|{2, 20}|{3, 20}|",
                "ACCOUNT", "DEPOSIT AMOUNT", "STATUS", "CURRENT BALANCE");
            Console.WriteLine(new String('-', 85));
            Console.Write("|{0, -20}|{1, 20}|", Account.Name, _amount.ToString("C"));
            Console.Write("{0, 20}|", Status);
            Console.WriteLine("{0, 20}|", Account.Balance.ToString("C"));
            Console.WriteLine(new String('-', 85));
        }

        public override void Execute()
        {
            base.Execute();

            _success = Account.Deposit(_amount);
            if (!_success)
            {
                throw new InvalidOperationException("Deposit amount invalid");
            }
        }

        public override void Rollback()
        {
            base.Rollback();

            bool complete = Account.Withdraw(_amount);
            if (!complete)
            {
                throw new InvalidOperationException("Insufficient funds to rollback");
            }
        }

        public override string GetAccountName() => Account.Name;
    }
}