using DatabaseHandler.Domain.Attributes;
using System;
using System.Reflection;

namespace DatabaseHandler.Model
{
    public class DbHandlerOracleSequence
    {
        public string Name { get; set; }
        public DbHandlerBulkOracleSequenceAttribute Attribute { get; set; }

        public Type PropertyType { get; set; }
    }
}
