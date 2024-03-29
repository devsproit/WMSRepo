﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using WMS.Core;
using Domain.Model.StockManagement;
using Domain.Model;
using Domain.Model.Masters;
using Microsoft.EntityFrameworkCore.Internal;

namespace Application.Services.StockMgnt
{
    public partial class ItemStockService : IItemStockService
    {
        #region Fields
        private readonly IRepository<InventoryStock> _itemStockRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<BranchWiseWarehouse> _branchWiseWarehouseRepository;
        #endregion

        #region Ctor
        public ItemStockService(IRepository<InventoryStock> itemStockRepository, IRepository<Branch> branchRepository, IRepository<BranchWiseWarehouse> branchWiseWarehouseRepository)
        {
            _itemStockRepository = itemStockRepository;
            _branchRepository = branchRepository;
            _branchWiseWarehouseRepository = branchWiseWarehouseRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(InventoryStock entity)
        {
            _itemStockRepository.Insert(entity);
        }

        public virtual void Update(InventoryStock entity)
        {
            _itemStockRepository.Update(entity);
        }

        public virtual void Update(List<InventoryStock> entities)
        {
            _itemStockRepository.Update(entities);
        }

        public virtual IPagedList<InventoryStock> GetItemStocks(int warehouse = 0, string itemCode = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _itemStockRepository.Table
                        select x;
            if (warehouse > 0)
                query = query.Where(x => x.WarehouseId == warehouse);
            if (!string.IsNullOrEmpty(itemCode))
                query = query.Where(x => x.ItemCode == itemCode);
            query = query.OrderByDescending(x => x.Id);
            var result = new PagedList<InventoryStock>(query, pageIndex, pageSize);
            return result;

        }
        public virtual IPagedList<InventoryStock> GetItemStocks(int branchId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var branch = from x in _branchWiseWarehouseRepository.Table
                         where x.BranchId == branchId
                         select x.WarehouseId;

            var query = from x in _itemStockRepository.Table

                        select x;
            query = query.Where(x => branch.Contains(x.WarehouseId));
            query = query.OrderByDescending(x => x.Id);
            var result = new PagedList<InventoryStock>(query, pageIndex, pageSize);
            return result;

        }

        public virtual InventoryStock GetById(int id)
        {
            return _itemStockRepository.GetById(id);
        }

        public virtual InventoryStock ItemByCode(string itemCode, int warehouse)
        {
            var query = from x in _itemStockRepository.Table
                        where x.ItemCode == itemCode && x.WarehouseId == warehouse
                        select x;


            return query.FirstOrDefault();

        }
        public virtual List<InventoryStock> ItemsByCode(string itemCode, int warehouse, bool avaibaleStock = true)
        {
            var query = from x in _itemStockRepository.Table
                        where x.ItemCode == itemCode && x.WarehouseId == warehouse
                        select x;
            if (avaibaleStock)
            {
                query = query.Where(x => x.Qty > 0);
            }
            

            return query.ToList();
        }
        #endregion
    }
}
