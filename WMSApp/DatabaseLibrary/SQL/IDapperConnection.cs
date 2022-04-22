using Dapper;
using System.Collections.Generic;

namespace DatabaseLibrary.SQL;
public interface IDapperConnection
{
    /// <summary>
    /// Execute stored procedure.
    /// </summary>
    /// <param name="storedProcName"> Stored Proc Name</param>
    /// <param name="p"> Sp Parameters</param>
    /// <returns>spReturnCount</returns>
    int? ExecuteStoredProc(string storedProcName, DynamicParameters p = null);

    /// <summary>
    /// Return List<T> object from using stored proc result.
    /// </summary>
    /// <typeparam name="T"> model class type</typeparam>
    /// <param name="storedProcName">Stored Proc Name</param>
    /// <param name="p"> Stored Proc parameters.</param>
    /// <returns></returns>
    List<T> ReadWithStoredProcedure<T>(string storedProcName, DynamicParameters p = null) where T : class;
}