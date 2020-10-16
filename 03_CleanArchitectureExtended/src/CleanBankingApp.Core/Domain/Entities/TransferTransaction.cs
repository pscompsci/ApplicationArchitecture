using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class TransferTransaction : Transaction
    {
        public Account From { get; private set; }
        public Account To { get; private set; }

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

        public override bool Rollback()
        {
            base.Rollback();
            To.Withdraw(Amount); // throws InsufficientFundsException
            Reversed = From.Deposit(Amount);
            if (!Reversed)
            {
                To.Deposit(Amount);
                return false;
            }
            return true;
        }
    }
}