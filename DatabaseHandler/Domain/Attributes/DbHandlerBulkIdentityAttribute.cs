using System;

namespace DatabaseHandler.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbHandlerBulkIdentityAttribute : Attribute
    {
        public DbHandlerBulkIdentityAttribute()
        {
        }
    }
}
