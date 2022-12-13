using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerBeginTransactionException : Exception
    {
        public DbHandlerBeginTransactionException(Exception innerException) : base("An error occurred while starting/getting the transaction", innerException) { }
    }
}
