﻿using Microsoft.AspNetCore.Mvc;
using Domain.Model.StockManagement;
using Application.Services.StockMgnt;
using WMS.Data;
using System.Linq;
using WMS.Web.Framework.Infrastructure.Extentsion;
using Application.Services;
using Application.Services.WarehouseMaster;
using WMSWebApp.ViewModels.Stock;
using System.Collections.Generic;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Controllers
{
    public class StockController : BaseAdminController
    {

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IItemStockService _itemStockService;
        private readonly IBranchHelper _branchService;
        private readonly IWarehouseService _warehouseService;
        private readonly ISubItemHelper _subItemService;


        #endregion

        #region Ctor
        public StockController(IWorkContext workContext, IItemStockService itemStockService, IWarehouseService warehouseService, ISubItemHelper subItemService, IBranchHelper branchService)
        {
            _workContext = workContext;
            _itemStockService = itemStockService;
            _warehouseService = warehouseService;
            _subItemService = subItemService;
            _branchService = branchService;
        }

        #endregion

        #region Methods
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }


        public IActionResult List()
        {
            var branch = _branchService.GetAllBranch();
            ViewBag.branch = branch;
            return View();
        }

        [HttpGet]
        public IActionResult WareHouse(int branchId)
        {
            var branch = _branchService.GetById(branchId);
            List<WarehouseModel> warehouseModel = new List<WarehouseModel>();
            foreach (var item in branch.BranchWiseWarehouses)
            {
                WarehouseModel warehouse = new WarehouseModel();
                warehouse.WarehouseName = item.Warehouse.WarehouseName;
                warehouse.WarehouseCode = item.Warehouse.WarehouseCode;
                warehouse.Id = item.Warehouse.Id;
                warehouseModel.Add(warehouse);
            }

            return Json(warehouseModel);
        }

        [HttpPost]
        public IActionResult List(DataSourceRequest request, int branchId, int warehouseId)
        {
            var branch = _branchService.GetBranchById(branchId);
            var warehouse = _warehouseService.GetById(warehouseId);
            var stocks = _itemStockService.GetItemStocks(warehouseId, "", request.Page - 1, request.PageSize);
            var dataGrid = new DataSourceResult()
            {
                Data = stocks.Select(x =>
                {
                    ItemStockList stock = new ItemStockList();
                    stock.Id = x.Id;
                    stock.Qty = x.Qty;
                    stock.ItemCode = x.ItemCode;
                    //var itemRemark = _goodReceivedNoteMasterService.GetGRNDetailsBySubItemCode(x.ItemCode);
                    //foreach (var i in itemRemark)
                    //{
                    //    if (i.Qty == x.Qty && i.AreaId == x.AreaId)
                    //    {
                    //        stock.Remark = i.Remark;
                    //    }
                    //}
                    stock.Remark = x.Remark;
                    stock.LastUpdateDate = x.LastUpdate.Date.ToShortDateString();
                    stock.Location = _warehouseService.GetWarehouseZoneAreaById(x.AreaId).AreaName;

                    var item = _subItemService.GetItemByCOde(x.ItemCode);
                    if (item != null)
                        stock.ItemName = item.SubItemName;
                    return stock;
                }),
                Total = stocks.TotalCount
            };
            return Json(dataGrid);

        }
        #endregion

    }
}
