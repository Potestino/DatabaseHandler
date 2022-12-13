using System;

namespace DatabaseHandler.Domain.Attributes
{
    /// <summary>
    /// [For Oracle usage only] - Generates a "sys_guid()" for the field. The field must be "byte[]" type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbHandlerBulkOracleGuidAttribute : Attribute
    {
        public DbHandlerBulkOracleGuidAttribute(bool hasTriggerAssociatedToGuid)
        {
            this.HasTriggerAssociatedToGuid = hasTriggerAssociatedToGuid;
        }

        public bool HasTriggerAssociatedToGuid { get; }
    }
}
