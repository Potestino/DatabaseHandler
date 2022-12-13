using DatabaseHandler.Domain.Attributes;
using DatabaseHandler.Domain.Exceptions;
using DatabaseHandler.Extensions;
using DatabaseHandler.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseHandler.Connections
{
    public class DbHandlerOracle : DbHandlerBase
    {
        public DbHandlerOracle(OracleConnection conn) : base(conn) { }

        public async Task BulkInsertAsync<T>(IEnumerable<T> values)
        {
            await Task.Run(() => 
            {
                string tableName = string.Empty;
                List<string> temporaryTablesToDispose = new List<string>();

                var sequenceDictionary = new List<DbHandlerOracleSequence>();
                Dictionary<string, DbHandlerBulkOracleGuidAttribute> guidDictionary = new Dictionary<string, DbHandlerBulkOracleGuidAttribute>();

                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                try
                {
                    ValidateProperties();
                    SetSequences();
                    SetGuids();
                    ExecuteBulkInsert();
                }
                catch (Exception ex)
                {
                    throw new DbHandlerBulkInsertException(tableName, ex);
                }
                finally
                {
                    DisposeTemporaryTables();
                }

                #region Bulk Insert private methods

                void ValidateProperties()
                {
                    var tableNameAttribute = typeof(T).GetCustomAttribute<DbHandlerBulkTableNameAttribute>();
                    if (tableNameAttribute == null)
                        throw new DbHandlerBulkTableNameAttributeNotImplementedException(typeof(T).Name);

                    tableName = tableNameAttribute.TableName;

                    foreach (var prop in properties)
                    {
                        var columnNameAttribute = prop.GetCustomAttribute<DbHandlerBulkColumnNameAttribute>();
                        if (columnNameAttribute == null) 
                            throw new DbHandlerBulkColumnNameAttributeNotImplementedException(prop.Name);

                        var sequenceAttribute = prop.GetCustomAttribute<DbHandlerBulkOracleSequenceAttribute>();
                        if (sequenceAttribute != null)
                        {
                            if (string.IsNullOrWhiteSpace(sequenceAttribute.SequenceName))
                                throw new DbHandlerBulkOracleSequenceAttributeException(prop.Name);

                            sequenceDictionary.Add(new DbHandlerOracleSequence 
                            {
                                Name = prop.Name, 
                                Attribute = sequenceAttribute,
                                PropertyType = prop.PropertyType
                            });
                        }

                        var guidAttribute = prop.GetCustomAttribute<DbHandlerBulkOracleGuidAttribute>();
                        if (guidAttribute != null)
                            guidDictionary.Add(prop.Name, guidAttribute);
                    }
                }

                void SetSequences()
                {
                    //Generate and set sequences for only those who don't have triggers associated
                    foreach (var sequenceItem in sequenceDictionary.Where(s => !s.Attribute.HasTriggerAssociatedToSequence))
                    {
                        var sequences = GenerateSequences(sequenceItem.Attribute.SequenceName);
                        var sequencesConverted = sequences.Select(e => Convert.ChangeType(e, sequenceItem.PropertyType));

                        for (int i = 0; i < values.Count(); i++)
                        {
                            values.ElementAt(i)
                                  .GetType()
                                  .GetProperty(sequenceItem.Name)
                                  .SetValue(values.ElementAt(i), sequencesConverted.ElementAt(i));
                        }
                    }
                }

                IEnumerable<string> GenerateSequences(string sequenceName)
                {
                    var now = DateTime.Now.ToString("yyMMddHHmmssffff");
                    var hash = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 3);
                    var sequenceTemporaryTableName = $"DBH_T_SEQ_{now}_{hash}";

                    //Create temporary sequence table
                    string createSequenceTemporaryTableNameSql = $"CREATE GLOBAL TEMPORARY TABLE {sequenceTemporaryTableName}(TEMP_ID NUMBER) ON COMMIT PRESERVE ROWS";
                    base.ExecuteAsync(createSequenceTemporaryTableNameSql).Wait();
                    temporaryTablesToDispose.Add(sequenceTemporaryTableName);

                    //Generate sequences into temporary sequence table
                    string generateSequenceSql = $@"declare
                                                        primaryKeyValue pls_integer;
                                                    begin
                                                        FOR i IN 1..{values.Count()} LOOP
                                                            select {sequenceName}.nextval
                                                                into primaryKeyValue
                                                                from dual;
                                                            INSERT INTO {sequenceTemporaryTableName} (TEMP_ID) VALUES (primaryKeyValue);
                                                        end loop;
                                                    end;";
                    base.ExecuteAsync(generateSequenceSql).Wait();

                    //Get sequences from temporary sequence table
                    return base.QueryAsync<string>($"SELECT TEMP_ID FROM {sequenceTemporaryTableName}").Result;
                }

                void SetGuids()
                {
                    //Generate and sets GUID's for each field with the attribute setted
                    foreach (var guidItem in guidDictionary.Where(g => !g.Value.HasTriggerAssociatedToGuid))
                    {
                        IEnumerable<byte[]> guids = GenerateGuids();

                        for (int i = 0; i < values.Count(); i++)
                        {
                            values.ElementAt(i)
                                  .GetType()
                                  .GetProperty(guidItem.Key)
                                  .SetValue(values.ElementAt(i), guids.ElementAt(i));
                        }
                    }
                }

                IEnumerable<byte[]> GenerateGuids()
                {
                    var now = DateTime.Now.ToString("yyMMddHHmmssffff");
                    var hash = Guid.NewGuid().ToString().ToUpperInvariant().Substring(0, 3);
                    var guidTemporaryTableName = $"DBH_T_SEQ_{now}_{hash}";

                    //Create temporary guid table
                    string createGuidTemporaryTableNameSql = $"CREATE GLOBAL TEMPORARY TABLE {guidTemporaryTableName}(TEMP_GUID RAW(16)) ON COMMIT PRESERVE ROWS";
                    base.ExecuteAsync(createGuidTemporaryTableNameSql).Wait();
                    temporaryTablesToDispose.Add(guidTemporaryTableName);

                    //Generate guid into temporary sequence table
                    string generateGuidSql = string.Format($@"declare
                                                               guid raw(16);
                                                            begin
                                                               FOR i IN 1..{values.Count()} LOOP
                                                                   select sys_guid()
                                                                        into guid
                                                                        from dual;
                                                                   INSERT INTO {guidTemporaryTableName}(TEMP_GUID) VALUES (guid);
                                                               end loop;
                                                            end;");
                    base.ExecuteAsync(generateGuidSql).Wait();

                    //Get sequences from temporary guid table
                    return base.QueryAsync<byte[]>($"SELECT TEMP_GUID FROM {guidTemporaryTableName}").Result;
                }

                void ExecuteBulkInsert()
                {
                    DataTable dataTable = values.ToDataTable(properties);

                    string hash = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                    string newTemporaryTableName = $"DBH_T_TB_{hash}";

                    //Create temporary table
                    base.ExecuteAsync($"CREATE GLOBAL TEMPORARY TABLE {newTemporaryTableName} ON COMMIT PRESERVE ROWS AS SELECT * FROM {tableName} WHERE 1=2").Wait();
                    temporaryTablesToDispose.Add(newTemporaryTableName);

                    //Execute bulk to temporary table
                    using var bulk = new OracleBulkCopy((OracleConnection)_db, OracleBulkCopyOptions.Default)
                    {
                        DestinationTableName = newTemporaryTableName,
                        BatchSize = 0
                    };
                    bulk.WriteToServer(dataTable);

                    //Transfer data from temporary table to final table
                    base.ExecuteAsync($"INSERT INTO {tableName} SELECT * FROM {newTemporaryTableName}").Wait();
                }

                void DisposeTemporaryTables()
                {
                    foreach (var tempTable in temporaryTablesToDispose)
                    {
                        try { base.ExecuteAsync($"TRUNCATE TABLE {tempTable}").Wait(); } catch { }
                        try { base.ExecuteAsync($"DROP TABLE {tempTable}").Wait(); } catch { }
                    }
                }

                #endregion
            });
        }
    }
}
