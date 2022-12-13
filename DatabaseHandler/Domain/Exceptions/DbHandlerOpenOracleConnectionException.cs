using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerOpenOracleConnectionException : Exception
    {
        public DbHandlerOpenOracleConnectionException(Exception innerException) : base("Error to open SQL Oracle connection", innerException) { }
    }
}
