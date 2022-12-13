using System;

namespace DatabaseHandler.Domain.Attributes
{
    /// <summary>
    /// [For Oracle usage only] - Generates a "nextval" from the sequence for the field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbHandlerBulkOracleSequenceAttribute : Attribute
    {
        public DbHandlerBulkOracleSequenceAttribute(string sequenceName, bool hasTriggerAssociatedToSequence)
        {
            this.SequenceName = sequenceName;
            this.HasTriggerAssociatedToSequence = hasTriggerAssociatedToSequence;
        }

        public string SequenceName { get; }
        public bool HasTriggerAssociatedToSequence { get; }
    }
}
