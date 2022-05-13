using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;
using System.Linq;
namespace DatabaseLibrary.SQL
{
    public class DapperConnection : IDapperConnection
    {
        private readonly string _server;
        private readonly string _database;
        private readonly string _tenantId;
        private readonly string _appId;
        private readonly string _appKey;
        private readonly string _connectionString;
        private readonly bool _isTokenBased;

        private AuthenticationResult token { get; set; }

        /// <summary>
        /// return SQL server Connection.
        /// </summary>
        /// <returns></returns>
        private IDbConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            if (_isTokenBased)
            {
                if (token == null || (token != null && token.ExpiresOn < DateTime.UtcNow))
                {
                    try
                    {
                        token = GetAccessToken();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                conn.AccessToken = token != null ? token.AccessToken : "";
            }
            return conn;
        }

        /// <summary>
        /// Generate Token for sql access.
        /// </summary>
        /// <returns></returns>
        private AuthenticationResult GetAccessToken()
        {
            AuthenticationResult token = null;
            try
            {
                var authContext = new AuthenticationContext(ConstantAndConfiguration.AadInstance + _tenantId);
                ClientCredential cc = new ClientCredential(_appId, _appKey);
                token = authContext.AcquireTokenAsync(ConstantAndConfiguration.SqlResourceUrl, cc).Result;
            }
            catch (Exception)
            {
                throw;
            }
            return token;
        }

        /// <summary>
        /// SQL dapper connection constructor using connection string.
        /// </summary>
        /// <param name="connectionString">SQL Server Connection String</param>
        public DapperConnection(string connectionString)
        {
            _connectionString = connectionString;
            _isTokenBased = false;
        }

        /// <summary>
        /// SQL dapper connection constructor for Toekn Based Auth.       
        /// </summary>
        /// <param name="server"> Sql Server Name</param>
        /// <param name="database">Database Name</param>
        /// <param name="tenantId">Tenant ID</param>
        /// <param name="appId">AAD App ID</param>
        /// <param name="appKey">App Key</param>
        public DapperConnection(string server, string database, string appId, string appKey, string tenantId = ConstantAndConfiguration.TenantId)
        {
            _server = server;
            _database = database;
            _tenantId = tenantId;
            _appId = appId;
            _appKey = appKey;
            _connectionString = $"Data Source={_server};Initial Catalog={_database}";
            _isTokenBased = true;
        }

        /// <summary>
        /// Return List<T> object from using stored proc result.
        /// </summary>
        /// <typeparam name="T"> model class type</typeparam>
        /// <param name="storedProcName">Stored Proc Name</param>
        /// <param name="p"> Stored Proc parameters.</param>
        /// <returns></returns>
        public List<T> ReadWithStoredProcedure<T>(string storedProcName, DynamicParameters p = null) where T : class
        {
            using (IDbConnection cnn = GetConnection())
            {
                var data = new List<T>();
                try
                {
                    var DT = cnn.Query<T>(storedProcName, p, commandType: CommandType.StoredProcedure);
                    data = DT.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                return data;
            }
        }

        /// <summary>
        /// Execute stored procedure.
        /// </summary>
        /// <param name="storedProcName"> Stored Proc Name</param>
        /// <param name="p"> Sp Parameters</param>
        /// <returns>spReturnCount</returns>
        public int? ExecuteStoredProc(string storedProcName, DynamicParameters p = null)
        {
            int? spReturnCount = null;
            using (IDbConnection cnn = GetConnection())
            {
                try
                {
                    spReturnCount = cnn.Execute(storedProcName, p, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return spReturnCount;
        }
    }
}