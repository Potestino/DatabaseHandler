using System;

namespace DatabaseHandler.Domain.Attributes
{
    /// <summary>
    /// The exact table's name in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbHandlerBulkTableNameAttribute : Attribute
    {
        public DbHandlerBulkTableNameAttribute(string tableName)
        {
            this.TableName = tableName;
        }

        public string TableName { get; }
    }
}
