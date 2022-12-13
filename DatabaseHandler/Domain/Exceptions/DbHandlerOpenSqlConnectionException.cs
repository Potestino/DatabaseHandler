using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerOpenSqlConnectionException : Exception
    {
        public DbHandlerOpenSqlConnectionException(Exception innerException) : base("Error to open SQL connection", innerException) { }
    }
}
