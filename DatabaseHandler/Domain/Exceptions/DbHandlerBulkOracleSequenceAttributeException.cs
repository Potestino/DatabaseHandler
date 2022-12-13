using DatabaseHandler.Domain.Attributes;
using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerBulkOracleSequenceAttributeException : Exception
    {
        public DbHandlerBulkOracleSequenceAttributeException(string propName) : base($"is necessary to inform the name sequential of the proprieties '{propName}'") { }
    }
}
