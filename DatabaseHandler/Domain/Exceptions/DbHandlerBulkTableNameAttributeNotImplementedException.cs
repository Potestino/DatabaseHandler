using DatabaseHandler.Domain.Attributes;
using System;

namespace DatabaseHandler.Domain.Exceptions
{
    public class DbHandlerBulkTableNameAttributeNotImplementedException : Exception
    {
        public DbHandlerBulkTableNameAttributeNotImplementedException(string className) : base($"Implement the attribute '{nameof(DbHandlerBulkTableNameAttribute)}' in the class '{className}'") { }
    }
}
