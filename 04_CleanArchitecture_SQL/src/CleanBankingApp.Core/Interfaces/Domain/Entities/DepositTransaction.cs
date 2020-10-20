using CleanBankingApp.Core.Domain.Exceptions;
using System;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class DepositTransaction : Transaction
    {
        public Account Account { get; set; }

        public DepositTransaction(Account account, decimal amount)
            : base(amount, "Deposit")
        {
            Account = account;
        }

        public override bool Execute()
        {
            base.Execute();
            Success = Account.Deposit(Amount);
            if (!Success) return false;
            return true;
        }

        public override Transaction Rollback()
        {
            if (!Success) throw new ArgumentOutOfRangeException(
                "Not yet successfully completed. Nothing to rollback");
            
            base.Rollback();
            Reversed = Account.Withdraw(Amount);

            if (!Reversed) 
                throw new InsufficientFundsException(
                    message: $"Insufficient funds to withdraw {Amount:C}!"
                );
                
            return  this;
        }
    }
}