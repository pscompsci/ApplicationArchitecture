﻿using System;
using System.Runtime.Serialization;

namespace CleanBankingApp.Infrastructure.Exceptions
{
    public class TransactionDoesNotExistException : Exception
    {
        public TransactionDoesNotExistException()
            : base(message: "Transaction does not exist")
        {
        }

        public TransactionDoesNotExistException(string message)
            : base(message)
        {
        }

        public TransactionDoesNotExistException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected TransactionDoesNotExistException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
