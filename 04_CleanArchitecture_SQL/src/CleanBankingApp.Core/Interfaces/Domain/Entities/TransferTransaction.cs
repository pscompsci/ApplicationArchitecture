using CleanBankingApp.Core.Domain.Exceptions;
using System;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class TransferTransaction : Transaction
    {
        public Account From { get; set; }
        public Account To { get; set; }

        public TransferTransaction(Account from, Account to, decimal amount)
            : base(amount, "Transfer")
        {
            From = from;
            To = to;
        }

        public override bool Execute()
        {
            base.Execute();
            From.Withdraw(Amount);  // throws InsufficientFundsException
            Success = To.Deposit(Amount);
            if (!Success)
            {
                From.Deposit(Amount);
                return false;
            }
            return true;
        }

        public override Transaction Rollback()
        {
            if (!Success) throw new ArgumentOutOfRangeException(
                "Not yet successfully completed. Nothing to rollback");
            
            base.Rollback();
            To.Withdraw(Amount); // throws InsufficientFundsException
            Reversed = From.Deposit(Amount);
            if (!Reversed)
            {
                To.Deposit(Amount);
                return this;
            }
            return this;
        }
    }
}