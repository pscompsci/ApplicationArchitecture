using System;

namespace CleanBankingApp.Core.Domain.Entities
{
    public abstract class Transaction
    {
        public int Id { get; private set; }
        public string Type { get; private set; }
        public decimal Amount { get; private set; }
        public bool Success { get; protected set; }
        public bool Executed { get; protected set; }
        public bool Reversed { get; protected set; }
        public DateTime DateStamp { get; private set; }
        public string Status 
        {
            get
            {
                if (Reversed) return "Reversed";
                else if (Executed && Success) return "Complete";
                else if (Executed) return "Incomplete";
                else return "Pending";
            }
        }

        public Transaction(decimal amount, string type)
        {
            if (amount >= 0) Amount = amount;
            Type = type;
        }

        public void SetId(int id) => Id = id;

        public virtual void Execute()
        {
            Executed = true;
            DateStamp = DateTime.Now;
        }

        public virtual void Rollback() => DateStamp = DateTime.Now;
    }
}
