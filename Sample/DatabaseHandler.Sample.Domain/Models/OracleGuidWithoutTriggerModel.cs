using DatabaseHandler.Domain.Attributes;

namespace DatabaseHandler.Sample.Domain.Models
{
    [DbHandlerBulkTableName("DBH_GUID_WITHOUT_TRIGGER")]
    public class OracleGuidWithoutTriggerModel
    {
        [DbHandlerBulkColumnName("Hash")]
        [DbHandlerBulkOracleGuid(hasTriggerAssociatedToGuid: false)]
        public byte[] Hash_Test { get; set; }

        [DbHandlerBulkColumnName("Description")]
        public string Description_Test { get; set; }

        [DbHandlerBulkColumnName("Value")]
        public int Value_Test { get; set; }

    }
}
