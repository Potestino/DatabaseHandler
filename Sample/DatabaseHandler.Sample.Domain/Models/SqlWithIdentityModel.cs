using DatabaseHandler.Domain.Attributes;

namespace DatabaseHandler.Sample.Domain.Models
{
    [DbHandlerBulkTableName("DBH_Tb_With_Identity")]
    public class SqlWithIdentityModel
    {
        [DbHandlerBulkColumnName("Id")]
        [DbHandlerBulkIdentity()]
        public decimal Id_Test { get; set; }

        [DbHandlerBulkColumnName("Description")]
        public string Description_Test { get; set; }

        [DbHandlerBulkColumnName("Value")]
        public int Value_Test { get; set; }
    }
}
