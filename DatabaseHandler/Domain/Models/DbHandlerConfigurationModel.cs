using DatabaseHandler.Domain.Enums;

namespace DatabaseHandler.Domain.Models
{
    public class DbHandlerConfigurationModel
    {
        public DbHandlerConfigurationModel(string connectionString, EDbHandlerConnectionType type)
        {
            this.ConnectionString = connectionString;
            this.Type = type;
        }

        public string ConnectionString { get; }
        public EDbHandlerConnectionType Type { get; }
    }
}
