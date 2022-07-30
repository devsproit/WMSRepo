using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using WMS.Core;
using Domain.Model.StockManagement;

namespace Application.Services.StockMgnt
{
    public partial class ItemStockService : IItemStockService
    {
        #region Fields
        private readonly IRepository<ItemStock> _itemStockRepository;
        #endregion

        #region Ctor
        public ItemStockService(IRepository<ItemStock> itemStockRepository)
        {
            _itemStockRepository = itemStockRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(ItemStock entity)
        {
            _itemStockRepository.Insert(entity);
        }

        public virtual void Update(ItemStock entity)
        {
            _itemStockRepository.Update(entity);
        }

        public virtual void Update(List<ItemStock> entities)
        {
            _itemStockRepository.Update(entities);
        }

        public virtual IPagedList<ItemStock> GetItemStocks(string branchCode = "", string itemCode = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _itemStockRepository.Table
                        select x;
            if (!string.IsNullOrEmpty(branchCode))
                query = query.Where(x => x.BranchCode == branchCode);
            if (!string.IsNullOrEmpty(itemCode))
                query = query.Where(x => x.ItemCode == itemCode);
            var result = new PagedList<ItemStock>(query, pageIndex, pageSize);
            return result;

        }

        public virtual ItemStock GetById(int id)
        {
            return _itemStockRepository.GetById(id);
        }

        public virtual ItemStock ItemByCode(string itemCode, string branchCode)
        {
            var query = from x in _itemStockRepository.Table
                        where x.ItemCode == itemCode && x.BranchCode == branchCode
                        select x;


            return query.FirstOrDefault();

        }
        #endregion
    }
}
