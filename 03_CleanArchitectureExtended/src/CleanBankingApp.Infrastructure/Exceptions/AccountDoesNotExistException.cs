using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace CleanBankingApp.Infrastructure.Exceptions
{
    public class AccountDoesNotExistException : Exception
    {
        public AccountDoesNotExistException()
            : base(message: "Account with given Id or Name does not exist")
        {
        }

        public AccountDoesNotExistException(string message)
            : base(message)
        {
        }

        public AccountDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
