using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary.SQL;
public interface IAdoConnection
{
    DataTable GetDatatableFromSqlWithSP(string SPName, List<SqlParameter> parameterlist = null);
    DataSet GetMutipleDatatableFromSqlWithSP(string SPName, List<SqlParameter> parameterlist = null);
    int InsertUpdateWithSP(string spName, List<SqlParameter> parameterlist);

}
