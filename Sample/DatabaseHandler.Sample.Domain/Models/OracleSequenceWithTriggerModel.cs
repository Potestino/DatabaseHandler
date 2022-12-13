using DatabaseHandler.Domain.Attributes;

namespace DatabaseHandler.Sample.Domain.Models
{
    [DbHandlerBulkTableName("DBH_SEQ_WITH_TRIGGER")]
    public class OracleSequenceWithTriggerModel
    {
        [DbHandlerBulkColumnName("Id")]
        [DbHandlerBulkOracleSequence(sequenceName: "DBH_SEQ_WITH_TRIGGER_SEQ", hasTriggerAssociatedToSequence: true)]
        public decimal Id_Test { get; set; }

        [DbHandlerBulkColumnName("Description")]
        public string Description_Test { get; set; }

        [DbHandlerBulkColumnName("Value")]
        public int Value_Test { get; set; }
    }
}
