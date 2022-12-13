using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using DatabaseHandler.Sample.Domain.Interfaces.Services;
using DatabaseHandler.Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.Service
{
    public class MassiveDataService : IMassiveDataService
    {
        private readonly IMassiveDataRepository _massiveRepository;
        public MassiveDataService(IMassiveDataRepository massiveRepository) => _massiveRepository = massiveRepository;

        //Sql
        public async Task InsertMassiveDataWithIdentityToSqlServer(int quantityToGenerate)
        {
            List<SqlWithIdentityModel> massiveDataWithIdentityList = new List<SqlWithIdentityModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                massiveDataWithIdentityList.Add(new SqlWithIdentityModel()
                {
                    Description_Test = $"Massive Item - With Identity - {i}",
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataWithIdentityToSqlServer(massiveDataWithIdentityList);
        }

        //Sql
        public async Task InsertMassiveDataWithoutIdentityToSqlServer(int quantityToGenerate)
        {
            List<SqlWithoutIdentityModel> massiveDataWithoutIdentityList = new List<SqlWithoutIdentityModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                massiveDataWithoutIdentityList.Add(new SqlWithoutIdentityModel()
                {
                    Guid_Test = Guid.NewGuid().ToString(),
                    Description_Test = $"Massive Item - Without Identity - {i}",
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataWithoutIdentityToSqlServer(massiveDataWithoutIdentityList);
        }

        //Oracle
        public async Task InsertMassiveDataSequenceWithTriggerToOracle(int quantityToGenerate)
        {
            List<OracleSequenceWithTriggerModel> massiveDataSequenceWithTriggerList = new List<OracleSequenceWithTriggerModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                string description = $"Massive Item - Sequence With Trigger - {i}";

                massiveDataSequenceWithTriggerList.Add(new OracleSequenceWithTriggerModel() 
                {
                    Description_Test = description,
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataSequenceWithTriggerToOracle(massiveDataSequenceWithTriggerList);
        }

        //Oracle
        public async Task InsertMassiveDataSequenceWithoutTriggerToOracle(int quantityToGenerate)
        {
            List<OracleSequenceWithoutTriggerModel> massiveDataSequenceWithoutTriggerList = new List<OracleSequenceWithoutTriggerModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                string description = $"Massive Item - Sequence Without Trigger - {i}";

                massiveDataSequenceWithoutTriggerList.Add(new OracleSequenceWithoutTriggerModel()
                {
                    Description_Test = description,
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataSequenceWithoutTriggerToOracle(massiveDataSequenceWithoutTriggerList);
        }

        //Oracle
        public async Task InsertMassiveDataGuidWithTriggerToOracle(int quantityToGenerate)
        {
            List<OracleGuidWithTriggerModel> massiveDataGuidWithTriggerList = new List<OracleGuidWithTriggerModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                string description = $"Massive Item - Guid With Trigger - {i}";

                massiveDataGuidWithTriggerList.Add(new OracleGuidWithTriggerModel()
                {
                    Description_Test = description,
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataGuidWithTriggerToOracle(massiveDataGuidWithTriggerList);
        }

        //Oracle
        public async Task InsertMassiveDataGuidWithoutTriggerToOracle(int quantityToGenerate)
        {
            List<OracleGuidWithoutTriggerModel> massiveDataGuidWithoutTriggerList = new List<OracleGuidWithoutTriggerModel>();

            for (int i = 1; i <= quantityToGenerate; i++)
            {
                string description = $"Massive Item - Guid Without Trigger - {i}";

                massiveDataGuidWithoutTriggerList.Add(new OracleGuidWithoutTriggerModel()
                {
                    Description_Test = description,
                    Value_Test = i
                });
            }

            await _massiveRepository.InsertMassiveDataGuidWithoutTriggerToOracle(massiveDataGuidWithoutTriggerList);
        }
    }
}
