using DatabaseHandler.Domain.Attributes;
using DatabaseHandler.Domain.Exceptions;
using DatabaseHandler.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseHandler.Connections
{
    public class DbHandlerSql : DbHandlerBase
    {
        public DbHandlerSql(SqlConnection conn) : base(conn) { }

        public async Task BulkInsertAsync<T>(IEnumerable<T> values, IDbTransaction transaction = null)
        {
            string tableName = string.Empty;
            try
            {
                var tableNameAttribute = typeof(T).GetCustomAttribute<DbHandlerBulkTableNameAttribute>();
                if (tableNameAttribute == null) throw new DbHandlerBulkTableNameAttributeNotImplementedException(typeof(T).Name);
                tableName = tableNameAttribute.TableName;

                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                DataTable dt = values.ToDataTable(properties);
                dt.TableName = tableName;

                using (var bulk = new SqlBulkCopy((SqlConnection)_db, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                {
                    bulk.DestinationTableName = tableNameAttribute.TableName;

                    //Bulk Mapping
                    foreach (var prop in properties)
                    {
                        var identityAttribute = prop.GetCustomAttribute<DbHandlerBulkIdentityAttribute>();
                        if (identityAttribute != null) continue;

                        var columnNameAttribute = prop.GetCustomAttribute<DbHandlerBulkColumnNameAttribute>();
                        if (columnNameAttribute == null) throw new DbHandlerBulkColumnNameAttributeNotImplementedException(prop.Name);

                        bulk.ColumnMappings.Add(prop.Name, columnNameAttribute.ColumnName);
                    }

                    await bulk.WriteToServerAsync(dt);
                }
            }
            catch (Exception ex)
            {
                throw new DbHandlerBulkInsertException(tableName, ex);
            }
        }
    }
}
