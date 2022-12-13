using Dapper;
using DatabaseHandler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DatabaseHandler.Connections
{
    public abstract class DbHandlerBase : IDisposable, IAsyncDisposable
    {
        protected IDbConnection _db;
        public DbHandlerBase(IDbConnection db) => _db = db;


        public virtual async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {                
                return await _db.QueryAsync(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar query: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.QueryAsync<T>(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar query: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.QueryFirstOrDefaultAsync(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar query: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.QueryFirstOrDefaultAsync<T>(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar query: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {                
                await _db.ExecuteAsync(sql, param: param, transaction: transaction);                
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar comando: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<dynamic> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.ExecuteScalarAsync(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar comando: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.ExecuteScalarAsync<T>(sql, param: param, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar comando: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<IEnumerable<dynamic>> ExecuteProcedureAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.QueryAsync(sql, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar procedure: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteProcedureAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                return await _db.QueryAsync<T>(sql, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao executar procedure: '{sql}' ||| Params: {param ?? "(vazio)"}", ex);
            }
        }

        public virtual IDbTransaction BeginTransaction()
        {
            try
            {
                return _db.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new DbHandlerBeginTransactionException(ex);
            }
        }

        #region Dispose/Async

        public void Dispose()
        {
            _db?.Dispose();
            _db = null;

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await disposeAsync();
            GC.SuppressFinalize(this);

            async ValueTask disposeAsync()
            {
                if (_db is IAsyncDisposable disposable)
                {
                    await disposable.DisposeAsync().ConfigureAwait(false);
                }
                else
                {
                    _db?.Dispose();
                }
                _db = null;
            }
        }

        #endregion
    }
}
