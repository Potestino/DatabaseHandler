using DatabaseHandler.Sample.Domain.Consts;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using DatabaseHandler.Sample.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Infra.Repositories
{
    public class MassiveDataRepository : IMassiveDataRepository
    {
        private readonly IDbHandlerFactory _db;
        public MassiveDataRepository(IDbHandlerFactory db) => _db = db;

        //Sql
        public async Task InsertMassiveDataWithIdentityToSqlServer(IEnumerable<SqlWithIdentityModel> values)
        {
            var sql = _db.GetSql(DbConsts.FOO_DATABASE_1);

            using var transaction = sql.BeginTransaction();
            {
                await sql.BulkInsertAsync(values, transaction);
                transaction.Commit();
            }
        }

        //Sql
        public async Task InsertMassiveDataWithoutIdentityToSqlServer(IEnumerable<SqlWithoutIdentityModel> values)
        {
            var sql = _db.GetSql(DbConsts.FOO_DATABASE_1);

            using var transaction = sql.BeginTransaction();
            {
                await sql.BulkInsertAsync(values, transaction);
                transaction.Commit();
            }
        }

        //Oracle
        public async Task InsertMassiveDataSequenceWithTriggerToOracle(IEnumerable<OracleSequenceWithTriggerModel> valuesSequenceWithTrigger)
        {
            await _db.GetOracle(DbConsts.FOO_DATABASE_3)
                     .BulkInsertAsync(valuesSequenceWithTrigger);
        }

        //Oracle
        public async Task InsertMassiveDataSequenceWithoutTriggerToOracle(IEnumerable<OracleSequenceWithoutTriggerModel> valuesSequenceWithoutTrigger)
        {
            await _db.GetOracle(DbConsts.FOO_DATABASE_3)
                     .BulkInsertAsync(valuesSequenceWithoutTrigger);
        }

        //Oracle
        public async Task InsertMassiveDataGuidWithTriggerToOracle(IEnumerable<OracleGuidWithTriggerModel> valuesGuidWithTrigger)
        {
            await _db.GetOracle(DbConsts.FOO_DATABASE_3)
                     .BulkInsertAsync(valuesGuidWithTrigger);
        }

        //Oracle
        public async Task InsertMassiveDataGuidWithoutTriggerToOracle(IEnumerable<OracleGuidWithoutTriggerModel> valuesGuidWithoutTrigger)
        {
            await _db.GetOracle(DbConsts.FOO_DATABASE_3)
                     .BulkInsertAsync(valuesGuidWithoutTrigger);
        }
    }
}
