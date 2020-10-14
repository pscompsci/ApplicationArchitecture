using System;

namespace CleanBankingApp
{
    abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;

        public bool Success { get => _success; }
        public bool Executed { get; private set; }
        public bool Reversed { get; private set; }
        public DateTime DateStamp { get; private set; }
        public string Type { get; private set; }
        public decimal Amount { get => _amount; }
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
            _amount = amount > 0 ? amount : throw new ArgumentOutOfRangeException("Amount must be > $0.00");
            Type = type;
        }

        public abstract string GetAccountName();

        public abstract void Print();

        public virtual void Execute()
        {
            if (Executed && _success)
            {
                throw new InvalidOperationException("Transaction previously executed");
            }
            DateStamp = DateTime.Now;
            Executed = true;
        }

        public virtual void Rollback()
        {
            if (Reversed)
            {
                throw new InvalidOperationException("Transaction already reversed");
            }
            else if (!_success)
            {
                throw new InvalidOperationException(
                    "Transaction not successfully executed. Nothing to rollback.");
            }
            DateStamp = DateTime.Now;
            Reversed = true;
        }
    }
}