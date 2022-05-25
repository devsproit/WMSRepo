using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary.SQL;
public class AdoConnection : IAdoConnection
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
    public AdoConnection(string connectionString)
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
    public AdoConnection(string server, string database, string appId, string appKey, string tenantId = ConstantAndConfiguration.TenantId)
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
    /// Get DataTable from Database using Stored Procedure ADO.Net connection.
    /// </summary>
    /// <param name="SPName"> Stored Procedure</param>
    /// <param name="parameterlist">SP parameters list(if any/nullable)</param>
    /// <returns>Data in DataTable object</returns>
    public DataTable GetDatatableFromSqlWithSP(string SPName, List<SqlParameter> parameterlist = null)
    {
        DataTable SqlDataTable = null;

        if (!string.IsNullOrEmpty(SPName))
        {
            using (IDbConnection Connector = GetConnection())
            {
                SqlDataTable = new DataTable();
                SqlCommand cmd = new SqlCommand(SPName, (SqlConnection)Connector);
                SqlDataAdapter CommandExecutor = new SqlDataAdapter();

                //Add Sp parameters if exist.
                if (parameterlist != null)
                {
                    cmd.Parameters.AddRange(parameterlist.ToArray());
                }
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    CommandExecutor.SelectCommand = cmd;
                    CommandExecutor.Fill(SqlDataTable);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        return SqlDataTable;
    }

    /// <summary>
    /// Insert/Update into Database using Stored Procedure ADO.Net connection.
    /// </summary>
    /// <param name="spName">Stored Procudure</param>
    /// <param name="parameterlist">SP parameters list(if any/nullable)</param>
    /// <returns>Count of records inserted/updated</returns>
    public int InsertUpdateWithSP(string spName, List<SqlParameter> parameterlist)
    {
        int result = 0;
        if (!string.IsNullOrEmpty(spName))
        {
            using (IDbConnection Connector = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(spName, (SqlConnection)Connector);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameterlist != null)
                {
                    cmd.Parameters.AddRange(parameterlist.ToArray());
                }
                try
                {
                    Connector.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Connector.Dispose();
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Get DataSet using Stored Procedure ADO.Net connection.
    /// </summary>
    /// <param name="spName">Stored Procudure</param>
    /// <param name="parameterlist">SP parameters list(if any/nullable)</param>
    /// <returns>Sql Data Set containing multiple data table</returns>
    public DataSet GetMutipleDatatableFromSqlWithSP(string SPName, List<SqlParameter> parameterlist = null)
    {
        DataSet SqlDataTable = null;

        if (!string.IsNullOrEmpty(SPName))
        {
            using (IDbConnection Connector = GetConnection())
            {
                SqlDataTable = new DataSet();
                SqlCommand cmd = new SqlCommand(SPName, (SqlConnection)Connector);
                SqlDataAdapter CommandExecutor = new SqlDataAdapter();

                //Add Sp parameters if exist.
                if (parameterlist != null)
                {
                    cmd.Parameters.AddRange(parameterlist.ToArray());
                }
                cmd.CommandType = CommandType.StoredProcedure;
                CommandExecutor.SelectCommand = cmd;
                try
                {
                    CommandExecutor.Fill(SqlDataTable);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        return SqlDataTable;
    }

    public DataTable bulkImport(string SPName, DataTable dt)
    {
        DataTable SqlDataTable = null;

        if (!string.IsNullOrEmpty(SPName))
        {
            using (IDbConnection Connector = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(SPName, (SqlConnection)Connector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tblTypeIntransit",dt);
                try
                {
                    Connector.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    Connector.Dispose();
                }
            }
        }
        return SqlDataTable;

    }
}