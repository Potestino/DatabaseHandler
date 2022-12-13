using DatabaseHandler.Sample.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Repositories
{
    public interface IMassiveDataRepository
    {
        //Sql
        Task InsertMassiveDataWithIdentityToSqlServer(IEnumerable<SqlWithIdentityModel> valuesWithIdentity);
        Task InsertMassiveDataWithoutIdentityToSqlServer(IEnumerable<SqlWithoutIdentityModel> valuesWithoutIdentity);

        //Oracle
        Task InsertMassiveDataSequenceWithTriggerToOracle(IEnumerable<OracleSequenceWithTriggerModel> valuesSequenceWithTrigger);
        Task InsertMassiveDataSequenceWithoutTriggerToOracle(IEnumerable<OracleSequenceWithoutTriggerModel> valuesSequenceWithoutTrigger);
        Task InsertMassiveDataGuidWithTriggerToOracle(IEnumerable<OracleGuidWithTriggerModel> valuesGuidWithTrigger);
        Task InsertMassiveDataGuidWithoutTriggerToOracle(IEnumerable<OracleGuidWithoutTriggerModel> valuesGuidWithoutTrigger);
    }
}
