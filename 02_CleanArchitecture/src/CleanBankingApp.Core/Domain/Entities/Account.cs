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
            if (balance < 0) return;
            Balance = balance;
        }

        public void SetId(int id) => Id = id;

        public bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;
            Balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0) return false;
            if (amount > Balance) return false;
            Balance -= amount;
            return true;
        }

    }
}
