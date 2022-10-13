using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using WMS.Core.Data;
using System.Collections.Generic;
using System.Linq;
using WMS.Core;
using System;

namespace Application.Services
{


    public class SubItemHelper : ISubItemHelper
    {
        private readonly IAdoConnection _adoConnection;
        private readonly IRepository<SubItemDb> _subItemRepository;

        public SubItemHelper(IAdoConnection adoConnection, IRepository<SubItemDb> subItemRepository)
        {
            _adoConnection = adoConnection;
            _subItemRepository = subItemRepository;
        }

        public List<SubItemDb> GetAllSubItem()
        {
            List<SubItemDb> data = new List<SubItemDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllSubItemSP);
            if (dbDT != null)
                data = dbDT.ToList<SubItemDb>();
            return data;
        }

        public SubItemDb GetSubItemById(int Id)
        {
            //List<SubItemDb> data = new List<SubItemDb>();
            //List<SqlParameter> sqlParameters = new List<SqlParameter>();
            //sqlParameters.Add(new SqlParameter("@Id", Id));
            //DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetSubItemIdSP, sqlParameters);
            //if (dbDT != null)
            //    data = dbDT.ToList<SubItemDb>();
            //return data.FirstOrDefault();
            return _subItemRepository.GetById(Id);
        }
        public bool CreateNewSubItem(SubItemDb SubItem)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (SubItem != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@SubItemCode", SubItem.SubItemCode),
                    new SqlParameter("@SubItemName", SubItem.SubItemName),
                    new SqlParameter("@ItemName", SubItem.ItemId),
                    new SqlParameter("@MaterialDescription", SubItem.MaterialDescription),
                    new SqlParameter("@SubItemSize", SubItem.SubItemSize),
                    new SqlParameter("@FOC", SubItem.FOC),
                    new SqlParameter("@SubItemCategory", SubItem.SubItemCategory),
                    new SqlParameter("@SubItemSR", SubItem.SubItemSR),

                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewSubItemSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool UpdateSubItemById(SubItemDb SubItem)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (SubItem != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                new SqlParameter("@Id", SubItem.Id),
                   new SqlParameter("@SubItemCode", SubItem.SubItemCode),
                    new SqlParameter("@SubItemName", SubItem.SubItemName),
                    new SqlParameter("@ItemName", SubItem.ItemId),
                    new SqlParameter("@MaterialDescription", SubItem.MaterialDescription),
                    new SqlParameter("@SubItemSize", SubItem.SubItemSize),
                    new SqlParameter("@FOC", SubItem.FOC),
                    new SqlParameter("@SubItemCategory", SubItem.SubItemCategory),
                    new SqlParameter("@SubItemSR", SubItem.SubItemSR),

                };
                result = _adoConnection.InsertUpdateWithSP(Constants.UpdateSubItemByIdSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool DeleteSubItemById(int Id)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            result = _adoConnection.InsertUpdateWithSP(Constants.DeleteSubItemByIdSP, sqlParameters);
            return result > 0 ? true : false;
        }


        public virtual void Insert(SubItemDb entity)
        {
            _subItemRepository.Insert(entity);
        }
        public virtual List<SubItemDb> GetSubItemByItemId(int id)
        {
            var query = from x in _subItemRepository.Table
                        where x.ItemId == id
                        select x;
            return query.ToList();

        }

        public virtual SubItemDb GetItemByCOde(string subItemCode)
        {
            var query = from x in _subItemRepository.Table
                        where x.SubItemCode == subItemCode
                        select x;
            return query.FirstOrDefault();
        }
        public SubItemDb GetSubItemCustomerAmt(string subItemName, string stype)
        {
            List<SubItemDb> data = new List<SubItemDb>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@subName", subItemName));
            sqlParameters.Add(new SqlParameter("@type", stype));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetSubItemCustAmtSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<SubItemDb>();
            return data.FirstOrDefault();
        }

        public virtual IPagedList<SubItemDb> GetAllTemp(string PoCat , int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _subItemRepository.Table
                        select x;
            query = query.Where(x => x.FOC == PoCat);
            var result = new PagedList<SubItemDb>(query, pageIndex, pageSize);
            return result;
        }

        public virtual IPagedList<SubItemDb> GetSubItem(string SubItemCode, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(SubItemCode))
                {
                    var query = from x in _subItemRepository.Table
                                select x;
                    if (SubItemCode == "0")
                    {

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(SubItemCode))
                        {
                            query = query.Where(x => x.SubItemCode == SubItemCode);
                        }
                    }
                 
                    var result = new PagedList<SubItemDb>(query, pageIndex, pageSize);
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}