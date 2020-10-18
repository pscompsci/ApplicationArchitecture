using System;
using System.Runtime.Serialization;
namespace CleanBankingApp.Core.Domain.Exceptions
{
    public class NegativeAmountException : Exception
    {
        public NegativeAmountException()
            : base(message: "Amount less than 0 is not possible.")
        {
        }

        public NegativeAmountException(string message)
            : base(message)
        {
        }

        public NegativeAmountException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected NegativeAmountException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
