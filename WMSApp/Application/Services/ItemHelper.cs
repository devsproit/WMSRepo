using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;
using WMS.Core.Data;
namespace Application.Services
{


    public class ItemHelper : IItemHelper
    {
        private readonly IAdoConnection _adoConnection;

        private readonly IRepository<ItemDb> _itemRepository;

        public ItemHelper(IAdoConnection adoConnection, IRepository<ItemDb> itemRepository)
        {
            _adoConnection = adoConnection;
            _itemRepository = itemRepository;
        }

        public List<ItemDb> GetAllItem()
        {
            List<ItemDb> data = new List<ItemDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllItemSP);
            if (dbDT != null)
                data = dbDT.ToList<ItemDb>();
            return data;
        }

        public ItemDb GetItemById(int Id)
        {
            List<ItemDb> data = new List<ItemDb>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetItemIdSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<ItemDb>();
            return data.FirstOrDefault();
        }
        public bool CreateNewItem(ItemDb Item)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Item != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@CompanyId", Item.CompanyId),
                    new SqlParameter("@ItemName", Item.ItemName),
                    new SqlParameter("@ItemCode", Item.ItemCode),
                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewItemSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool UpdateItemById(ItemDb Item)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Item != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                new SqlParameter("@Id", Item.Id),
                   new SqlParameter("@CompanyName", Item.CompanyId),
                    new SqlParameter("@ItemName", Item.ItemName),
                    new SqlParameter("@ItemCode", Item.ItemCode),
                };
                result = _adoConnection.InsertUpdateWithSP(Constants.UpdateItemByIdSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool DeleteItemById(int Id)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            result = _adoConnection.InsertUpdateWithSP(Constants.DeleteItemByIdSP, sqlParameters);
            return result > 0 ? true : false;
        }


        public virtual void Update(ItemDb entity)
        {
            _itemRepository.Update(entity);
        }


    }

}