﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WMSWebApp.Models;
using Application.Services;
using WMSWebApp.ViewModels.Dashboard;
using Application.Services.GRN;
using WMS.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels;
using Domain.Model.GRN;
using Application.Services.StockMgnt;
using WMSWebApp.ViewModels.Stock;
using Application.Services.WarehouseMaster;
using Application.Services.Report;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIntrasitService _intrasitService;
        private readonly IWarehouseService _wareHouseService;
        private readonly IWorkContext _workContext;
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IItemStockService _itemStockService;
        private readonly ISubItemHelper _subItemService;
        private readonly IReportService _reportService;
        public HomeController(ILogger<HomeController> logger, IIntrasitService intrasitService, IWarehouseService wareHouseService, IWorkContext workContext, IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IItemStockService itemStockService, ISubItemHelper subItemService, IReportService reportService)
        {
            _logger = logger;
            _intrasitService = intrasitService;
            _wareHouseService = wareHouseService;
            _workContext = workContext;
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _itemStockService = itemStockService;
            _subItemService = subItemService;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var branch = await _workContext.GetCurrentBranch();
            DashboardCountModel model = new DashboardCountModel();
            var pendingGRN = _intrasitService.GetPendingPO(branch.BranchCode, "0");
            if (pendingGRN != null)
            {
                model.GRN = pendingGRN.Count;
            }
            else
            {
                model.GRN = 0;
            }
            model.PendingPickUp = _reportService.PendingPickUp();
            model.PendingDispatch = _reportService.PendingDispatch();
            model.PendingInvoice = _reportService.PendingInvoice();

            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Stock()
        {
            var branch = await _workContext.GetCurrentBranch();
            var stocks = _goodReceivedNoteMasterService.GetStocks(branch.BranchCode);
            int id = 1;
            var data = new DataSourceResult()
            {
                Data = stocks.Select(x =>
                {
                    var m = new Stocks();

                    m.Id = id;
                    m.SubItemName = x.SubItemName;
                    m.Qty = x.Qty;
                    m.ItemCode = x.ItemCode;
                    m.Remark = x.Remark;
                    id++;
                    return m;
                }),
                Total = stocks.TotalCount
            };

            return Json(data);
        }

        [HttpPost]
        public virtual async Task<IActionResult> StockList(DataSourceRequest request)
        {
            var branch = await _workContext.GetCurrentBranch();
            var stocks = _itemStockService.GetItemStocks(branch.Id, request.Page - 1, request.PageSize);
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
                    stock.Location = _wareHouseService.GetWarehouseZoneAreaById(x.AreaId).AreaName;

                    var item = _subItemService.GetItemByCOde(x.ItemCode);
                    if (item != null)
                        stock.ItemName = item.SubItemName;
                    return stock;
                }),
                Total = stocks.TotalCount
            };
            return Json(dataGrid);
        }
    }
}
