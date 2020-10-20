using CleanBankingApp.Core.Domain.Exceptions;
using System;

namespace CleanBankingApp.Core.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool Success { get; set; }
        public bool Executed { get; set; }
        public bool Reversed { get; set; }
        public DateTime DateStamp { get; set; }
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
            if (amount < 0) throw new NegativeAmountException();
            Amount = amount;
            Type = type;
        }

        public void SetId(int id) => Id = id;

        public virtual bool Execute()
        {
            Executed = true;
            DateStamp = DateTime.Now;
            return true;
        }

        public virtual Transaction Rollback()
        {
            DateStamp = DateTime.Now;
            return this;
        }
    }
}
