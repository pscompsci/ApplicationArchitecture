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

        public override bool Execute()
        {
            base.Execute();
            Success = Account.Withdraw(Amount);
            if (!Success)
                throw new InsufficientFundsException(
                    message: $"Insufficient funds to withdraw {Amount.ToString("C")}!"
                );
            return true;
        }

        public override bool Rollback()
        {
            base.Rollback();
            Account.Deposit(Amount);
            return true;
        }
    }
}