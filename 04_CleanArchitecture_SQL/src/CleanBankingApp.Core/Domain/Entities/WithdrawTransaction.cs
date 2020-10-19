using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class WithdrawTransaction : Transaction
    {
        public Account Account { get; set; }

        public WithdrawTransaction(Account account, decimal amount)
            : base(amount, "Withdraw")
        {
            Account = account;
        }

        public override bool Execute()
        {
            base.Execute();
            Success = Account.Withdraw(Amount);
            if (!Success)
                throw new InsufficientFundsException(
                    message: $"Insufficient funds to withdraw {Amount:C}!"
                );
            return true;
        }

        public override bool Rollback()
        {
            if (!Success) return false;
            
            base.Rollback();
            Reversed = Account.Deposit(Amount);
            return Reversed;
        }
    }
}