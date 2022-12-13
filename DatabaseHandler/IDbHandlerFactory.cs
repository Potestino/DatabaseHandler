using DatabaseHandler.Connections;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace DatabaseHandler
{
    public interface IDbHandlerFactory
    {        
        /// <summary>
        /// Get handler (Dapper) SQL to be automatic managed
        /// </summary>
        DbHandlerSql GetSql(string key);

        /// <summary>
        /// Get handler (Dapper) SQL Oracle to be automatic managed
        /// </summary>
        DbHandlerOracle GetOracle(string key);

        /// <summary>
        /// Get handler (Dapper) SQL Oracle without automatic managed
        /// </summary>
        /// <return>SqlConnection</return>
        SqlConnection GetSqlUnmanaged(string key);

        /// <summary>
        /// Get handler (Dapper) SQL Oracle without automatic managed
        /// </summary>
        /// <return>OracleConnection</return>
        OracleConnection GetOracleUnmanaged(string key);
    }
}
