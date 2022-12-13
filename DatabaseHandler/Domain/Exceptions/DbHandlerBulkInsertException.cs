using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerBulkInsertException : Exception
    {
        public DbHandlerBulkInsertException(string tableName, Exception innerException) : base($"An error occurred while performing the bulk insert. ||| TableName: {tableName}", innerException) { }
    }
}
