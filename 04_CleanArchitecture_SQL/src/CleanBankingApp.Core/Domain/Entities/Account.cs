using CleanBankingApp.Core.Domain.Exceptions;
using System.Collections.Generic;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public Account() {}

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
