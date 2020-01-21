using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CoreLib.Connections
{
    public interface IDapperConnection
    {
        void OpenConnection([CallerMemberName] string property = null);
        void CloseConnection([CallerMemberName] string property = null);

        void BeginTransaction([CallerMemberName] string property = null);
        void CommitTransaction([CallerMemberName] string property = null);
        void RollBackTransaction([CallerMemberName] string property = null);


        Task<TEntity> QueryOneAsync<TEntity>(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null);

        Task<IEnumerable<TEntity>> QueryManyAsync<TEntity>(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null);

        Task<int> ExecuteAsync(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null);
    }

    public sealed class DapperConnection : IDapperConnection
    {
        /// <summary>
        /// Conexão
        /// </summary>
        private readonly IDbConnection _connection;

        /// <summary>
        /// Transação
        /// </summary>
        private IDbTransaction _transaction;



        public DapperConnection(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }



        /// <summary>
        /// Open the Connection
        /// </summary>
        /// <param name="property"><seealso cref="CallerMemberNameAttribute"/></param>
        /// <exception cref="ArgumentException"><seealso cref="IDbConnection.State"/></exception>
        /// <exception cref="ArgumentNullException"><seealso cref="IDbConnection"/></exception>
        public void OpenConnection([CallerMemberName] string property = null)
        {
            try
            {
                switch (_connection.State)
                {
                    case ConnectionState.Open:
                        return;
                    case ConnectionState.Closed:
                        _connection.Open();
                        break;
                    case ConnectionState.Connecting:
                        throw new ArgumentException($"[{DateTime.Now}] [ DapperConnection::OpenConnection ]: Try Connecting...", nameof(_connection));
                    default:
                        throw new ArgumentException($"[{DateTime.Now}] [ DapperConnection::OpenConnection ]: DbConnection is null or Connection State not defined!", nameof(_connection));
                }
            }
            catch
            {
                CloseConnection();
                throw;
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{DateTime.Now}] DapperConnection::OpenConnection is called by '{property}'.");
                Console.ResetColor();
            }
        }

        public void CloseConnection([CallerMemberName] string property = null)
        {
            try
            {
                _connection.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{DateTime.Now}] DapperConnection::CloseConnection is called by '{property}'.");
                Console.ResetColor();
            }
        }



        public void BeginTransaction([CallerMemberName] string property = null)
        {
            try
            {
                OpenConnection();

                if (_transaction is null || _transaction.Connection != _connection)
                {
                    _transaction = _connection.BeginTransaction();
                }
            }
            catch
            {
                CloseConnection();
                throw;
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{DateTime.Now}] DapperConnection::BeginTransaction is called by '{property}'.");
                Console.ResetColor();
            }
        }

        public void CommitTransaction([CallerMemberName] string property = null)
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{DateTime.Now}] DapperConnection::CommitTransaction is called by '{property}'.");
                Console.ResetColor();

                CloseConnection();
            }
        }

        public void RollBackTransaction([CallerMemberName] string property = null)
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{DateTime.Now}] DapperConnection::RollBackTransaction is called by '{property}'.");
                Console.ResetColor();

                CloseConnection();
            }
        }



        public async Task<TEntity> QueryOneAsync<TEntity>(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null)
        {
            try
            {
                OpenConnection();
                return await _connection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, parameters, _transaction);
            }
            catch
            {
                throw;
            }
            finally
            {
                if(_transaction is null)
                {
                    CloseConnection();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> QueryManyAsync<TEntity>(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null)
        {
            try
            {
                OpenConnection();
                return await _connection.QueryAsync<TEntity>(sqlCommand, parameters, _transaction);
            }
            catch
            {
                throw;
            }
            finally
            {
                if(_transaction is null)
                {
                    CloseConnection();
                }
            }
        }

        public async Task<int> ExecuteAsync(string sqlCommand, object parameters = null, [CallerMemberName] string callerMethod = null)
        {
            try
            {
                OpenConnection();
                return await _connection.ExecuteAsync(sqlCommand, parameters, _transaction);
            }
            catch
            {
                throw;
            }
            finally
            {
                if(_transaction is null)
                {
                    CloseConnection();
                }
            }
        }
    }
}