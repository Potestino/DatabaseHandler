using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Domain.Interfaces.Services
{
    public interface IMassiveDataService
    {
        //Sql
        Task InsertMassiveDataWithIdentityToSqlServer(int quantityToGenerate);
        Task InsertMassiveDataWithoutIdentityToSqlServer(int quantityToGenerate);

        //Oracle
        Task InsertMassiveDataSequenceWithTriggerToOracle(int quantityToGenerate);
        Task InsertMassiveDataSequenceWithoutTriggerToOracle(int quantityToGenerate);
        Task InsertMassiveDataGuidWithTriggerToOracle(int quantityToGenerate);
        Task InsertMassiveDataGuidWithoutTriggerToOracle(int quantityToGenerate);
    }
}
