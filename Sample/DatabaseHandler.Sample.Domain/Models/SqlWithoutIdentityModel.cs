using DatabaseHandler.Domain.Attributes;

namespace DatabaseHandler.Sample.Domain.Models
{
    [DbHandlerBulkTableName("DBH_Tb_Without_Identity")]
    public class SqlWithoutIdentityModel
    {
        [DbHandlerBulkColumnName("Guid")]
        public string Guid_Test { get; set; }

        [DbHandlerBulkColumnName("Description")]
        public string Description_Test { get; set; }

        [DbHandlerBulkColumnName("Value")]
        public int Value_Test { get; set; }
    }
}
