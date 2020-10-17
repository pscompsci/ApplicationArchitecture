using System;
using System.Runtime.Serialization;

namespace CleanBankingApp.Infrastructure.Exceptions
{
    public class AccountDoesNotExistException : Exception
    {
        public AccountDoesNotExistException()
            : base(message: "Account does not exist")
        {
        }

        public AccountDoesNotExistException(string message)
            : base(message)
        {
        }

        public AccountDoesNotExistException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected AccountDoesNotExistException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
