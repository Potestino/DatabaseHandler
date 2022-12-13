using DatabaseHandler.Domain.Attributes;
using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerBulkColumnNameAttributeNotImplementedException : Exception
    {
        public DbHandlerBulkColumnNameAttributeNotImplementedException(string propName) : base($"Is necessary to implement the attribute '{nameof(DbHandlerBulkColumnNameAttribute)}' on the property '{propName}'") { }
    }
}
