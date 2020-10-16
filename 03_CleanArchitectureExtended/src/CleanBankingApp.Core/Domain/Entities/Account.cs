using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class Account
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string name, decimal balance = 0)
        {
            Name = name;
            if (balance < 0) throw new NegativeAmountException(
                "Balance set to $0.00");
            Balance = balance;
        }

        public void SetId(int id) => Id = id;

        public bool Deposit(decimal amount)
        {
            if (amount < 0) throw new NegativeAmountException();
            Balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount < 0) throw new NegativeAmountException();
            if (amount > Balance) throw new InsufficientFundsException();
            Balance -= amount;
            return true;
        }

    }
}
