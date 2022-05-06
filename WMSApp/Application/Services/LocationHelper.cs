using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;

namespace Application.Services;
public class LocationHelper : ILocationHelper
{
    private readonly IAdoConnection _adoConnection;

    public LocationHelper(IAdoConnection adoConnection)
    {
        _adoConnection = adoConnection;
    }

    public List<LocationDb> GetAllLocation()
    {
        List<LocationDb> data = new List<LocationDb>();
        DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllLocationsSP);
        if (dbDT != null)
            data = dbDT.ToList<LocationDb>();
        return data;
    }

    public LocationDb GetLocationById(int Id)
    {
        List<LocationDb> data = new List<LocationDb>();
        List<SqlParameter> sqlParameters = new List<SqlParameter>();
        sqlParameters.Add(new SqlParameter("@Id", Id));
        DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetLocationIdSP, sqlParameters);
        if (dbDT != null)
            data = dbDT.ToList<LocationDb>();
        return data.FirstOrDefault();
    }
    public bool CreateNewLocation(LocationDb Location)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = null;
        if (Location != null)
        {
            sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScreenCode", Location.ScreenCode),
                    new SqlParameter("@LocationCode", Location.LocationCode),
                    new SqlParameter("@LocationName", Location.LocationName),
                    
                };
            result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewLocationSP, sqlParameters);
        }
        return result > 0 ? true : false;
    }
    public bool UpdateLocationById(LocationDb Location)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = null;
        if (Location != null)
        {
            sqlParameters = new List<SqlParameter>()
                {
                   new SqlParameter("@ScreenCode", Location.ScreenCode),
                    new SqlParameter("@LocationCode", Location.LocationCode),
                    new SqlParameter("@LocationName", Location.LocationName),
                    
                };
            result = _adoConnection.InsertUpdateWithSP(Constants.UpdateLocationByIdSP, sqlParameters);
        }
        return result > 0 ? true : false;
    }
    public bool DeleteLocationById(int Id)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = new List<SqlParameter>();
        sqlParameters.Add(new SqlParameter("@Id", Id));
        result = _adoConnection.InsertUpdateWithSP(Constants.DeleteLocationByIdSP, sqlParameters);
        return result > 0 ? true : false;
    }


}
