using DatabaseHandler.Domain.Attributes;

namespace DatabaseHandler.Sample.Domain.Models
{
    [DbHandlerBulkTableName("DBH_GUID_WITH_TRIGGER")]
    public class OracleGuidWithTriggerModel
    {
        [DbHandlerBulkColumnName("Hash")]
        [DbHandlerBulkOracleGuid(hasTriggerAssociatedToGuid: true)]
        public byte[] Hash_Test { get; set; }

        [DbHandlerBulkColumnName("Description")]
        public string Description_Test { get; set; }

        [DbHandlerBulkColumnName("Value")]
        public int Value_Test { get; set; }

    }
}
