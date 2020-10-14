using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class WithdrawTransaction : Transaction
    {
        public Account Account { get; private set; }

        public WithdrawTransaction(Account account, decimal amount)
            : base(amount, "Withdraw")
        {
            Account = account;
        }

        public override void Execute()
        {
            base.Execute();
            Success = Account.Withdraw(Amount);
            if (!Success)
                throw new InsufficientFundsException(
                    message: $"Insufficient funds to withdraw {Amount.ToString("C")}!"
                );
        }

        public override void Rollback()
        {
            base.Rollback();
            Account.Deposit(Amount);
        }
    }
}