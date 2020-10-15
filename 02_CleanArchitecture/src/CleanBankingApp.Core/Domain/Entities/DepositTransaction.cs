using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class DepositTransaction : Transaction
    {
        public Account Account { get; private set; }

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

        public override bool Rollback()
        {
            base.Rollback();
            Reversed = Account.Withdraw(Amount);
            if (!Reversed) 
                throw new InsufficientFundsException(
                    message: $"Insufficient funds to withdraw {Amount.ToString("C")}!"
                );
            return true;
        }
    }
}