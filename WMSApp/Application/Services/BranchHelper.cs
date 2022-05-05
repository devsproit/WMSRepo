﻿using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;

namespace Application.Services;
public class BranchHelper : IBranchHelper
{
    private readonly IAdoConnection _adoConnection;

    public BranchHelper(IAdoConnection adoConnection)
    {
        _adoConnection = adoConnection;
    }

    public List<BranchDb> GetAllBranch()
    {
        List<BranchDb> data = new List<BranchDb>();
        DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllBranchSP);
        if (dbDT != null)
            data = dbDT.ToList<BranchDb>();
        return data;
    }

    public BranchDb GetBranchById(int Id)
    {
        List<BranchDb> data = new List<BranchDb>();
        List<SqlParameter> sqlParameters = new List<SqlParameter>();
        sqlParameters.Add(new SqlParameter("@Id", Id));
        DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetBranchIdSP, sqlParameters);
        if (dbDT != null)
            data = dbDT.ToList<BranchDb>();
        return data.FirstOrDefault();
    }
    public bool CreateNewBranch(BranchDb Branch)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = null;
        if (Branch != null)
        {
            sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScreenCode", Branch.ScreenCode),
                    new SqlParameter("@BranchCode", Branch.BranchCode),
                    new SqlParameter("@BranchName", Branch.BranchName),
                    new SqlParameter("@CompanyName", Branch.CompanyName),
                    new SqlParameter("@Address", Branch.Address),
                    new SqlParameter("@Location", Branch.Location),
                    new SqlParameter("@ContactPersonBranch", Branch.ContactPersonBranch),
                    new SqlParameter("@ContactNumberBranch", Branch.ContactNumberBranch),
                    new SqlParameter("@EmailIdBranch", Branch.EmailIdBranch),
                    new SqlParameter("AssociatedEmployee", Branch.AssociatedEmployee),
                    new SqlParameter("@WarehouseName", Branch.WarehouseName),
                    
                };
            result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewBranchSP, sqlParameters);
        }
        return result > 0 ? true : false;
    }
    public bool UpdateBranchById(BranchDb Branch)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = null;
        if (Branch != null)
        {
            sqlParameters = new List<SqlParameter>()
                {
                   new SqlParameter("@ScreenCode", Branch.ScreenCode),
                    new SqlParameter("@BranchCode", Branch.BranchCode),
                    new SqlParameter("@BranchName", Branch.BranchName),
                    new SqlParameter("@CompanyName", Branch.CompanyName),
                    new SqlParameter("@Address", Branch.Address),
                    new SqlParameter("@Location", Branch.Location),
                    new SqlParameter("@ContactPersonBranch", Branch.ContactPersonBranch),
                    new SqlParameter("@ContactNumberBranch", Branch.ContactNumberBranch),
                    new SqlParameter("@EmailIdBranch", Branch.EmailIdBranch),
                    new SqlParameter("AssociatedEmployee", Branch.AssociatedEmployee),
                    new SqlParameter("@WarehouseName", Branch.WarehouseName),
                };
            result = _adoConnection.InsertUpdateWithSP(Constants.UpdateBranchByIdSP, sqlParameters);
        }
        return result > 0 ? true : false;
    }
    public bool DeleteBranchById(int Id)
    {
        int result = 0;
        List<SqlParameter> sqlParameters = new List<SqlParameter>();
        sqlParameters.Add(new SqlParameter("@Id", Id));
        result = _adoConnection.InsertUpdateWithSP(Constants.DeleteBranchByIdSP, sqlParameters);
        return result > 0 ? true : false;
    }

    
}