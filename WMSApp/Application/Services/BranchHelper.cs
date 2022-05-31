using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System.Linq;
using WMS.Core.Data;
using WMS.Core;
namespace Application.Services
{


    public class BranchHelper : IBranchHelper
    {
        private readonly IAdoConnection _adoConnection;
        private readonly IRepository<Branch> _branchRepository;
        public BranchHelper(IAdoConnection adoConnection, IRepository<Branch> branchRepository)
        {
            _adoConnection = adoConnection;
            _branchRepository = branchRepository;
        }

        public List<Branch> GetAllBranch()
        {
            List<Branch> data = new List<Branch>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllBranchSP);
            if (dbDT != null)
                data = dbDT.ToList<Branch>();
            return data;
        }

        public Branch GetBranchById(int Id)
        {
            List<Branch> data = new List<Branch>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetBranchIdSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<Branch>();
            return data.FirstOrDefault();
        }
        public bool CreateNewBranch(Branch Branch)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Branch != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    
                    new SqlParameter("@BranchCode", Branch.BranchCode),
                    new SqlParameter("@BranchName", Branch.BranchName),
                    new SqlParameter("@CompanyName", Branch.CompanyId),
                    new SqlParameter("@Address", Branch.Address),
                    new SqlParameter("@Location", Branch.Location),
                    new SqlParameter("@ContactPersonBranch", Branch.ContactPersonBranch),
                    new SqlParameter("@ContactNumberBranch", Branch.ContactNumberBranch),
                    new SqlParameter("@EmailIdBranch", Branch.EmailIdBranch),
                    new SqlParameter("AssociatedEmployee", Branch.AssociatedEmployee),
                    //new SqlParameter("@WarehouseName", Branch.WarehouseId),

                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewBranchSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool UpdateBranchById(Branch Branch)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Branch != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                   
                    new SqlParameter("@BranchCode", Branch.BranchCode),
                    new SqlParameter("@BranchName", Branch.BranchName),
                    new SqlParameter("@CompanyName", Branch.CompanyId),
                    new SqlParameter("@Address", Branch.Address),
                    new SqlParameter("@Location", Branch.Location),
                    new SqlParameter("@ContactPersonBranch", Branch.ContactPersonBranch),
                    new SqlParameter("@ContactNumberBranch", Branch.ContactNumberBranch),
                    new SqlParameter("@EmailIdBranch", Branch.EmailIdBranch),
                    new SqlParameter("AssociatedEmployee", Branch.AssociatedEmployee),
                    //new SqlParameter("@WarehouseName", Branch.WarehouseId),
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

        public void Insert(Branch entity)
        {
            _branchRepository.Insert(entity);
        }
        public virtual IPagedList<Branch> GetAllBranches(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _branchRepository.Table
                        select x;
            query = query.OrderByDescending(x => x.Id);
            return new PagedList<Branch>(query, pageIndex, pageSize);


        }

        public virtual Branch GetById(int id)
        {
            return _branchRepository.GetById(id);   
        }

    }
}
