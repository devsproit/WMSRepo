﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Application.Services.PO;
using WMS.Data;
using WMSAPI.ViewModels.Invoice;
using System.Collections.Generic;
using Application.Services.Invoice;
using Domain.Model.Invoice;
using WMS.Web.Framework.Infrastructure.Extentsion;
using Domain.Model.PO;
using Domain.Model.StockManagement;
using Application.Services.StockMgnt;
using Application.Services.PS;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.WarehouseMaster;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;

namespace WMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class InvoiceController : BaseAdminController
    {

        #region Fields
        private readonly IPurchaseOrder _purchaseOrderService;
        private readonly ISalePo _salePoService;
        private readonly IServiceOrderPo _serviceOrderPoService;
        private readonly IStockTransferPo _stockTransferPoService;
        private readonly IWorkContext _workContext;
        private readonly IInvoiceService _invoiceService;
        private readonly IItemStockService _itemStockService;
        private readonly IPickSlipService _pickSlipService;
        private readonly ISubItemHelper _subItemService;
        private readonly IWarehouseService _warehouseService;
        #endregion

        #region Ctor
        public InvoiceController(IPurchaseOrder purchaseOrderService, ISalePo salePoService, IServiceOrderPo serviceOrderPoService, IStockTransferPo stockTransferPoService, IWorkContext workContext, IInvoiceService invoiceService, IItemStockService itemStockService, IPickSlipService pickSlipService, ISubItemHelper subItemService, IWarehouseService warehouseService)
        {
            _purchaseOrderService = purchaseOrderService;
            _salePoService = salePoService;
            _serviceOrderPoService = serviceOrderPoService;
            _stockTransferPoService = stockTransferPoService;
            _workContext = workContext;
            _invoiceService = invoiceService;
            _itemStockService = itemStockService;
            _pickSlipService = pickSlipService;
            _subItemService = subItemService;
            _warehouseService = warehouseService;
        }
        #endregion

        #region Methods
        [NonAction]
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [NonAction]
        public IActionResult List()
        {
            return View();
        }

        [HttpPost("List")]
        public async Task<IActionResult> List(DataSourceRequest request)
        {
            var branch = await _workContext.GetCurrentBranch();
            var data = _invoiceService.GetAllMaster(branch.BranchCode, request.Page - 1, request.PageSize, "ALL");
            DataSourceResult gridData = new DataSourceResult
            {
                Data = data.Select(x =>
                {
                    InvoiceListModel m = new InvoiceListModel();
                    m.PoNumber = x.PoNumber;
                    m.PoCategory = x.PoCategory;
                    m.CreateOn = x.CreateOn;
                    m.InvoiceNumber = x.PoNumber;
                    m.BilledTo = x.BilledTo;
                    m.Id = x.Id;
                    return m;

                }),
                Total = data.TotalCount,
            };

            return Json(gridData);

        }
        [HttpPost("InvoiceList")]
        public IActionResult InvoiceList(DataSourceRequest request)
        {
            List<InvoiceListModel> m = new List<InvoiceListModel>();
            DataSourceResult model = new DataSourceResult
            {
                Data = m,
                Total = m.Count
            };
            return Json(model);
        }
        [NonAction]
        public IActionResult Create()
        {
            return View();
        }

        [NonAction]
        [HttpPost]
        public IActionResult Create(string id)
        {
            return Json("");
        }

        [HttpGet("GetItems")]
        public virtual IActionResult GetItems(int id)
        {
            var pickslip = _pickSlipService.GetbyId(id);
            List<InvoiceItemModel> model = new List<InvoiceItemModel>();
            if (pickslip.DockType == "StockTransfer PO")
            {
                var items = pickslip.PickSlipDetails;
                foreach (var item in items)
                {
                    InvoiceItemModel m = new InvoiceItemModel();

                    m.Amount = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }
                    var ponumber = _salePoService.GetById(pickslip.POID);
                    m.PoNumber = ponumber.PONumber;
                    m.Qty = item.Qty;
                    model.Add(m);
                }
            }
            else if (pickslip.DockType == "Sale PO")
            {
                var items = pickslip.PickSlipDetails;
                foreach (var item in items)
                {
                    InvoiceItemModel m = new InvoiceItemModel();

                    m.Amount = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }
                    var ponumber = _salePoService.GetById(pickslip.POID);
                    m.PoNumber = ponumber.PONumber;
                    m.Qty = item.Qty;
                    model.Add(m);
                }
            }
            else
            {
                var items = pickslip.PickSlipDetails;
                foreach (var item in items)
                {
                    InvoiceItemModel m = new InvoiceItemModel();

                    m.Amount = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }
                    var ponumber = _serviceOrderPoService.GetById(pickslip.POID);
                    m.PoNumber = ponumber.PONumber;
                    m.Qty = item.Qty;
                    model.Add(m);
                }
            }
            return Json(model);

        }

        [NonAction]
        [HttpPost]
        public virtual IActionResult Save([FromBody] PickSlipToInvoiceModel model)
        {
            InvoiceMaster master = new InvoiceMaster();
            List<InvoiceDetails> details = new List<InvoiceDetails>();
            var branch = _workContext.GetCurrentBranch().Result;
            var pickslip = _pickSlipService.GetbyId(model.PickSlipId);

            if (pickslip.DockType == "StockTransfer PO")
            {
                var items = pickslip.PickSlipDetails;
                var ponumber = _stockTransferPoService.GetById(pickslip.POID);
                master.PoNumber = ponumber.PONumber;
                master.PoCategory = pickslip.DockType;
                master.CreateOn = DateTime.Now;
                master.BilledTo = ponumber.StockTransferPOSendingTo;
                master.PickSlipId = pickslip.Id;
                master.InvoiceNumber = model.InvoiceId;
                master.BranchCode = branch.BranchCode;
                master.DispatchDone = false;
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails();

                    m.Amt = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    //m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }


                    m.Qty = item.Qty;
                    m.InvoiceMaster = master;
                    master.InvoiceDetails.Add(m);

                }

                _invoiceService.InsertMaster(master);
                pickslip = _pickSlipService.GetbyId(model.PickSlipId);
                pickslip.IsProcessed = true;
                _pickSlipService.Update(pickslip);
            }
            else if (pickslip.DockType == "Sale PO")
            {
                var items = pickslip.PickSlipDetails;
                var ponumber = _salePoService.GetById(pickslip.POID);
                master.PoNumber = ponumber.PONumber;
                master.PoCategory = pickslip.DockType;
                master.CreateOn = DateTime.Now;
                master.BilledTo = ponumber.SalePOSendingTo;
                master.PickSlipId = pickslip.Id;
                master.InvoiceNumber = model.InvoiceId;
                master.BranchCode = branch.BranchCode;
                master.DispatchDone = false;
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails();

                    m.Amt = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    //m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }


                    m.Qty = item.Qty;
                    m.InvoiceMaster = master;
                    master.InvoiceDetails.Add(m);

                }

                _invoiceService.InsertMaster(master);
                pickslip = _pickSlipService.GetbyId(model.PickSlipId);
                pickslip.IsProcessed = true;
                _pickSlipService.Update(pickslip);
            }
            else
            {
                var items = pickslip.PickSlipDetails;
                var ponumber = _serviceOrderPoService.GetById(pickslip.POID);
                master.PoNumber = ponumber.PONumber;
                master.PoCategory = pickslip.DockType;
                master.CreateOn = DateTime.Now;
                master.BilledTo = ponumber.ServiceOrderPOSendingTo;
                master.PickSlipId = pickslip.Id;
                master.InvoiceNumber = model.InvoiceId;
                master.BranchCode = branch.BranchCode;
                master.DispatchDone = false;
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails();

                    m.Amt = item.Amount;
                    m.SubItemName = item.SubItemName;
                    m.AreaId = item.AreaId;
                    //m.Id = item.Id;
                    m.SubItemCode = item.SubItemCode;
                    var subItem = _subItemService.GetItemByCOde(item.SubItemCode);
                    if (subItem != null)
                    {
                        m.MaterialDescription = subItem.MaterialDescription;
                    }
                    var area = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);
                    if (area != null)
                    {
                        m.AreaCode = area.AreaCode;
                        m.AreaName = area.AreaName;
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.ZoneCode = zone.ZoneCode;
                        m.ZoneName = zone.ZoneName;
                        var warehouse = _warehouseService.GetById(zone.WarehouseId);
                        m.Warehouse = warehouse.WarehouseName;
                        m.WarehouseCode = warehouse.WarehouseCode;
                    }


                    m.Qty = item.Qty;
                    m.InvoiceMaster = master;
                    master.InvoiceDetails.Add(m);

                }

                _invoiceService.InsertMaster(master);
                pickslip = _pickSlipService.GetbyId(model.PickSlipId);
                pickslip.IsProcessed = true;
                _pickSlipService.Update(pickslip);
            }
            return Json(true);
        }



        [HttpGet("GetPickSlip")]
        public virtual IActionResult GetPickSlip()
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var result = _pickSlipService.GetPickSlipMasters(branch.BranchCode, "", 0, int.MaxValue, true);
            List<PickSlipListModel> model = new List<PickSlipListModel>();
            foreach (var item in result.ToList())
            {
                PickSlipListModel pickSlip = new PickSlipListModel();
                pickSlip.Id = item.Id;
                model.Add(pickSlip);
            }
            return Json(model);
        }

        [NonAction]
        public virtual IActionResult Print(int id)
        {
            return View();
        }
        #endregion


        #region Utilities
        protected void ReduceUpdateStock(string itemCode, int warehouse, int qty)
        {
            var stock = _itemStockService.ItemByCode(itemCode, warehouse);
            if (stock != null)
            {
                var item = _itemStockService.GetById(stock.Id);
                item.Qty = item.Qty - qty;
                item.LastUpdate = DateTime.Now;
                _itemStockService.Update(item);
            }
            else
            {
                var item = new InventoryStock();
                item.WarehouseId = warehouse;
                item.ItemCode = itemCode;
                item.LastUpdate = DateTime.Now;
                item.Qty = qty;
                _itemStockService.Insert(item);
            }
        }
        #endregion

    }
}
