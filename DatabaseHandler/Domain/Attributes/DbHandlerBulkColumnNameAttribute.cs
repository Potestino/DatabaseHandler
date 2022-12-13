using System;

namespace DatabaseHandler.Domain.Attributes
{
    /// <summary>
    /// The exact column's name in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbHandlerBulkColumnNameAttribute : Attribute
    {
        public DbHandlerBulkColumnNameAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
        public string ColumnName { get; }
    }
}
