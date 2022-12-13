using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Dapper.SqlMapper;

namespace DatabaseHandler.Helpers
{
    public class DbHandlerDynamicParameters : IDynamicParameters
    {
        private readonly DynamicParameters _dynamicParameters;
        private readonly List<OracleParameter> _oracleParameters;
        public DbHandlerDynamicParameters()
        {
            _dynamicParameters = new DynamicParameters();
            _oracleParameters = new List<OracleParameter>();
        }

        //General
        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            _dynamicParameters.Add(name, value, dbType, direction, size);
        }

        //Oracle
        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            _oracleParameters.Add(oracleParameter);
        }

        //Oracle
        public void Add(string name, object value, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, value, direction);
            _oracleParameters.Add(oracleParameter);
        }

        [Obsolete("Function used only per Dapper. Do not execute it!")]
        public void AddParameters(IDbCommand command, Identity identity)
        {
            ((IDynamicParameters)_dynamicParameters).AddParameters(command, identity);

            if (command is OracleCommand oracleCommand)
            {
                oracleCommand.Parameters.AddRange(_oracleParameters.ToArray());
            }
        }
    }
}
