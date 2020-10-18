using System;

namespace CleanBankingApp.Core.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException()
            : base(message: "Insufficient funds in the account.")
        {
        }

        public InsufficientFundsException(string message)
            : base(message)
        {
        }

        public InsufficientFundsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}