using DatabaseHandler.Connections;
using DatabaseHandler.Domain.Enums;
using DatabaseHandler.Domain.Exceptions;
using DatabaseHandler.Domain.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseHandler.Core
{
    public class DbHandlerFactory : IDbHandlerFactory,
                                    IDisposable,
                                    IAsyncDisposable
    {
        #region Constructor & Variables

        private ConcurrentDictionary<string, DbHandlerConfigurationModel> _configuration { get; set; }
        private ConcurrentDictionary<string, DbHandlerSql> _dictionarySqlHandlers;
        private ConcurrentDictionary<string, DbHandlerOracle> _dictionaryOracleHandlers;
        private ConcurrentBag<DbHandlerSql> _sqlHandlersToDispose;
        private ConcurrentBag<DbHandlerOracle> _oracleHandlersToDispose;

        public DbHandlerFactory(ConcurrentDictionary<string, DbHandlerConfigurationModel> configuration)
        {
            if (configuration == null) throw new ArgumentException("At least one connection must be informed.");
            if (configuration.Any(c => string.IsNullOrWhiteSpace(c.Key))) throw new ArgumentException("You must enter a value for the key");
            if (configuration.Any(c => string.IsNullOrWhiteSpace(c.Value.ConnectionString))) throw new ArgumentException("It is necessary to inform a value for the connectionString.");

            _configuration = configuration;
            _dictionarySqlHandlers = new ConcurrentDictionary<string, DbHandlerSql>();
            _dictionaryOracleHandlers = new ConcurrentDictionary<string, DbHandlerOracle>();

            _sqlHandlersToDispose = new ConcurrentBag<DbHandlerSql>();
            _oracleHandlersToDispose = new ConcurrentBag<DbHandlerOracle>();
        }

        #endregion

        public DbHandlerSql GetSql(string key)
        {
            var configuration = GetConfiguration(key);
            if (configuration.Type != EDbHandlerConnectionType.Sql) throw new ArgumentException($"The informed key  '{key}' is not of type '{EDbHandlerConnectionType.Sql}'.");

            if (_dictionarySqlHandlers.ContainsKey(key))
            {
                return _dictionarySqlHandlers.First(c => c.Key == key).Value;
            }
            else
            {
                var newSqlConn = CreateSqlConnection(configuration.ConnectionString);
                var newSqlHandler = new DbHandlerSql(newSqlConn);

                _dictionarySqlHandlers.TryAdd(key, newSqlHandler);
                _sqlHandlersToDispose.Add(newSqlHandler);

                return newSqlHandler;
            }
        }

        public DbHandlerOracle GetOracle(string key)
        {
            var configuration = GetConfiguration(key);
            if (configuration.Type != EDbHandlerConnectionType.Oracle) throw new ArgumentException($"The informed key  '{key}' is not of type '{EDbHandlerConnectionType.Oracle}'.");

            if (_dictionaryOracleHandlers.ContainsKey(key))
            {
                return _dictionaryOracleHandlers.First(c => c.Key == key).Value;
            }
            else
            {
                var newOracleConn = CreateOracleConnection(configuration.ConnectionString);
                var newOracleHandler = new DbHandlerOracle(newOracleConn);

                _dictionaryOracleHandlers.TryAdd(key, newOracleHandler);
                _oracleHandlersToDispose.Add(newOracleHandler);

                return newOracleHandler;
            }
        }

        public SqlConnection GetSqlUnmanaged(string key)
        {
            var configuration = GetConfiguration(key);
            if (configuration.Type != EDbHandlerConnectionType.Sql) throw new ArgumentException($"The informed key  '{key}' is not of type '{EDbHandlerConnectionType.Sql}'.");

            return CreateSqlConnection(configuration.ConnectionString);
        }

        public OracleConnection GetOracleUnmanaged(string key)
        {
            var configuration = GetConfiguration(key);
            if (configuration.Type != EDbHandlerConnectionType.Oracle) throw new ArgumentException($"The informed key  '{key}' is not of type '{EDbHandlerConnectionType.Oracle}'.");

            return CreateOracleConnection(configuration.ConnectionString);
        }

        #region Private Methods

        private DbHandlerConfigurationModel GetConfiguration(string key)
        {
            var config = _configuration.GetValueOrDefault(key);
            if (config == null) throw new ArgumentException($"The informed key '{key}' does not exist in the list of connections.");
            return config;
        }

        private SqlConnection CreateSqlConnection(string connectionString)
        {
            try
            {
                var newSqlConn = new SqlConnection(connectionString);
                newSqlConn.Open();
                return newSqlConn;
            }
            catch (Exception ex)
            {
                throw new DbHandlerOpenSqlConnectionException(ex);
            }
        }

        private OracleConnection CreateOracleConnection(string connectionString)
        {
            try
            {
                var newOracleConn = new OracleConnection(connectionString);
                newOracleConn.Open();
                return newOracleConn;
            }
            catch (Exception ex)
            {
                throw new DbHandlerOpenOracleConnectionException(ex);
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            foreach (var sql in _sqlHandlersToDispose)
            {
                sql.Dispose();
            }
            foreach (var oracle in _oracleHandlersToDispose)
            {
                oracle.Dispose();
            }

            _sqlHandlersToDispose = null;
            _oracleHandlersToDispose = null;

            _dictionarySqlHandlers = null;
            _dictionaryOracleHandlers = null;

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await disposeAsync();

            _sqlHandlersToDispose = null;
            _oracleHandlersToDispose = null;

            _dictionarySqlHandlers = null;
            _dictionaryOracleHandlers = null;

            GC.SuppressFinalize(this);

            async ValueTask disposeAsync()
            {
                foreach (var sql in _sqlHandlersToDispose)
                {
                    if (sql is IAsyncDisposable disposable)
                        await disposable.DisposeAsync().ConfigureAwait(false);
                    else
                        sql.Dispose();
                }

                foreach (var oracle in _oracleHandlersToDispose)
                {
                    if (oracle is IAsyncDisposable disposable)
                        await disposable.DisposeAsync().ConfigureAwait(false);
                    else
                        oracle.Dispose();
                }
            }
        }

        #endregion
    }
}
