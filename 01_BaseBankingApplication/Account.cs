using System;

namespace Task_7_1P
{
    class Account
    {
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string name, decimal balance = 0)
        {
            Name = name;
            if (balance < 0) return;
            Balance = balance;
        }

        public bool Deposit(decimal amount)
        {
            if ((amount < 0) || (amount == decimal.MaxValue))
                return false;

            Balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if ((amount < 0) || (amount > Balance))
                return false;

            Balance -= amount;
            return true;
        }

        public void Print()
        {
            Console.WriteLine("Account Name: {0}, Balance: {1}",
                Name, Balance.ToString("C"));
        }
    }
}
