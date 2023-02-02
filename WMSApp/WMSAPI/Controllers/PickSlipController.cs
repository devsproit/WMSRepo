using Microsoft.AspNetCore.Mvc;
using Application.Services.PS;
using Application.Services.GRN;
using Microsoft.AspNetCore.Authorization;
using Domain.Model.PS;
using WMS.Web.Framework.Infrastructure.Extentsion;
using System;
using System.Linq;
using Application.Services.WarehouseMaster;
using WMSAPI.ViewModels.Pickslip;
using WMS.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using WMSAPI.ViewModels.GRN;
using Application.Services.PO;
using Application.Services.StockMgnt;
using Domain.Model;
using Domain.Model.Masters;
using Domain.Model.StockManagement;

namespace WMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PickSlipController : BaseAdminController
    {

        #region Fields
        private readonly ITempPickSlipDetailsService _tempPickSlipDetailsService;
        private readonly IPickSlipService _pickSlipService;
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IWorkContext _workContext;
        private readonly IItemStockService _itemStockService;
        private readonly IPurchaseOrder _purchaseOrder;
        private readonly ISalePo _salePoService;
        private readonly IServiceOrderPo _serviceOrderPoService;
        private readonly IStockTransferPo _stockTransferPoService;


        #endregion

        #region Ctor
        public PickSlipController(ITempPickSlipDetailsService tempPickSlipDetailsService, IPickSlipService pickSlipService, IWarehouseService warehouseService, IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IWorkContext workContext, IPurchaseOrder purchaseOrder, ISalePo salePoService, IServiceOrderPo serviceOrderPoService, IStockTransferPo stockTransferPoService, IItemStockService itemStockService)
        {
            _tempPickSlipDetailsService = tempPickSlipDetailsService;
            _pickSlipService = pickSlipService;
            _warehouseService = warehouseService;
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _workContext = workContext;
            _purchaseOrder = purchaseOrder;
            _salePoService = salePoService;
            _serviceOrderPoService = serviceOrderPoService;
            _stockTransferPoService = stockTransferPoService;
            _itemStockService = itemStockService;
        }
        #endregion

        #region Methods

        [NonAction]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost("List")]

        public virtual IActionResult List(DataSourceRequest request, string guid)
        {
            List<PickslipDetailModel> result = new List<PickslipDetailModel>();
            // var result = _tempPickSlipDetailsService.GetAllTemp(guid, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = result,

                Total = result.Count,
            };

            return Json(gridData);
        }

        [HttpGet("GrnList")]
        public virtual IActionResult GrnList()
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var pos = _goodReceivedNoteMasterService.GetAllMaster(branch.BranchCode).ToList();
            List<GrnListModel> list = new List<GrnListModel>();
            list = pos.Select(x => new GrnListModel() { GRNId = x.Id }).ToList();
            return Json(list);
        }


        [HttpGet("PoList")]
        public virtual IActionResult PoList(string docType)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var pos = _purchaseOrder.GetPurchaseOrders(branch.BranchCode, docType);
            List<GrnListModel> list = new List<GrnListModel>();
            list = pos.Select(x => new GrnListModel() { PoNumber = x.PONumber, GRNId = x.Id }).ToList();
            return Json(list);
        }

        //[HttpGet]
        //public virtual IActionResult GetGrnProduct(int id,string category)
        //{
        //    var grnitems = _purchaseOrder.GetById(id);
        //    if(category=="")
        //    var items = grnitems.GoodReceivedNoteDetails.ToList();
        //    List<GrnItemListModel> model = new List<GrnItemListModel>();
        //    foreach (var item in items)
        //    {
        //        GrnItemListModel m = new GrnItemListModel()
        //        {
        //            Amount = item.Amount,
        //            SubItemName = item.SubItemName,
        //            AreaId = item.AreaId,
        //            GRNId = item.GRNId,
        //            Id = item.Id,
        //            ItemCode = item.ItemCode,
        //            MaterialDescription = item.MaterialDescription,
        //            Qty = item.Qty,
        //            SubItemCode = item.SubItemCode,
        //            Unit = item.Unit,
        //        };
        //        model.Add(m);
        //    }
        //    return Json(model);
        //}


        [HttpGet("GetPoProduct")]
        public virtual IActionResult GetPoProduct(string id, string docType)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            List<GrnItemListModel> model = new List<GrnItemListModel>();
            if (docType == "StockTransfer PO")
            {
                var items = _stockTransferPoService.GetStockTransferPos(id);

                foreach (var item in items)
                {
                    GrnItemListModel m = new GrnItemListModel();

                    m.Amount = 0;
                    m.SubItemName = item.StockTransferPOSubItem;
                    m.POId = id;
                    m.Id = item.Id;
                    m.ItemCode = item.StockTransferPOItem;
                    m.MaterialDescription = "";
                    m.Qty = item.StockTransferPOQty;
                    m.SubItemCode = item.SubItemCode;
                    m.Location = "";
                    m.Unit = "";
                    model.Add(m);
                }
            }
            else if (docType == "Sale PO")
            {
                var items = _salePoService.GetSalePos(id);
                foreach (var item in items)
                {
                    GrnItemListModel m = new GrnItemListModel();

                    m.Amount = Convert.ToInt32(item.SalePOAmt);
                    m.SubItemName = item.SalePOSubItem;

                    m.POId = id;
                    m.Id = item.Id;
                    m.ItemCode = item.SalePOSubItem;
                    var location = _itemStockService.ItemsByCode(item.SubItemCode, branch.BranchWiseWarehouses.FirstOrDefault().WarehouseId);
                    if (location != null)
                    {
                        var warehouse = _warehouseService.GetWarehouseZoneAreaById(location.FirstOrDefault().AreaId);
                        m.Location = warehouse.AreaName;
                        m.AreaId = warehouse.Id;
                        m.InventoryId = location.FirstOrDefault().Id;
                    }
                    m.MaterialDescription = "";
                    m.Qty = item.SalePOQty;
                    m.SubItemCode = item.SubItemCode;
                    m.Unit = "";




                    model.Add(m);
                }
            }
            else

            {
                var items = _serviceOrderPoService.GetServicePos(id);
                foreach (var item in items)
                {
                    GrnItemListModel m = new GrnItemListModel()
                    {
                        Amount = item.ServiceOrderPOQty,
                        SubItemName = item.ServiceOrderPOSubitem,
                        Id = item.Id,
                        ItemCode = item.SubItemCode,
                        MaterialDescription = "",
                        Qty = item.ServiceOrderPOQty,
                        SubItemCode = item.SubItemCode,
                        Location = "",
                        POId = id,
                    };
                    model.Add(m);
                }
            }
            return Json(model);

        }
        [NonAction]
        [HttpGet]
        public virtual IActionResult GetLocation(string SubItemCode)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var location = _itemStockService.ItemsByCode(SubItemCode, branch.BranchWiseWarehouses.FirstOrDefault().WarehouseId);
            List<LocationList> locations = new List<LocationList>();
            if (location != null)
            {
                foreach (var item in location)
                {
                    LocationList locationList = new LocationList();
                    var warehouse = _warehouseService.GetWarehouseZoneAreaById(item.AreaId);

                    locationList.Location = warehouse.AreaName;
                    locationList.AreaId = warehouse.Id;
                    locationList.InventoryId = item.Id;
                    locations.Add(locationList);
                }

            }
            return Json(locations);
        }

        [NonAction]
        [HttpPost]
        public virtual IActionResult Save([FromBody] List<GrnItemListModel> model)
        {
            if (model.Count > 0)
            {
                var branch = _workContext.GetCurrentBranch().Result;
                PickSlipMaster master = new PickSlipMaster();
                master.PickSlipName = "";
                master.CreateOn = DateTime.Now;
                master.BranchCode = branch.BranchCode;
                master.POID = model.FirstOrDefault().Id;
                master.DockType = model.FirstOrDefault().DockType;
                foreach (var item in model)
                {
                    PickSlipDetails details = new PickSlipDetails();
                    details.Qty = item.Qty;
                    details.SubItemName = item.SubItemName;
                    details.AreaId = item.AreaId;
                    details.Amount = item.Amount;
                    details.InventoryId = item.InventoryId;
                    details.ItemCode = item.ItemCode;
                    details.PickSlipMaster = master;
                    details.SubItemCode = item.SubItemCode;
                    details.Unit = item.Unit;
                    master.PickSlipDetails.Add(details);

                }
                _pickSlipService.Insert(master);
                foreach (var item in model)
                {
                    switch (item.DockType)
                    {
                        case "ServiceOrder PO":
                            {
                                var pos = _serviceOrderPoService.GetById(item.Id);
                                pos.IsProcessed = true;
                                _serviceOrderPoService.Update(pos);
                                break;
                            }
                        case "Sale PO":
                            {
                                var pos = _salePoService.GetById(item.Id);
                                pos.IsProcessed = true;
                                _salePoService.Update(pos);
                                break;
                            }
                        case "StockTransfer PO":
                            {
                                var pos = _stockTransferPoService.GetById(item.Id);
                                pos.IsProcessed = true;
                                _stockTransferPoService.Update(pos);
                                break;
                            }

                    }

                }
                // Update Inventory
                foreach (var item in model)
                {
                    AddUpdateStock(item.InventoryId, item.Qty);
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        [NonAction]
        public virtual IActionResult PickSlipList()
        {
            return View();

        }

        [NonAction]
        [HttpPost("PickSlipList")]
        public IActionResult PickSlipList(DataSourceRequest request)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var result = _pickSlipService.GetPickSlipMasters(branch.BranchCode, "", request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = result.Select(x =>
                {
                    PickSlipListModel m = new PickSlipListModel();
                    m.PickSlipName = x.PickSlipName;
                    m.BranchCode = x.BranchCode;
                    m.Branch = x.BranchCode;
                    m.DockType = x.DockType;
                    m.CreateOn = x.CreateOn;
                    m.Id = x.Id;
                    m.Item = x.PickSlipDetails.Count;

                    return m;
                }),

                Total = result.TotalCount,
            };

            return Json(gridData);
        }
        [NonAction]
        public IActionResult pickSlipPrint(int id)
        {
            //var pickslip = _pickSlipService.GetbyId(id);
            //if (pickslip.DockType == "Sale PO")
            //{

            //}
            return View(id);
        }
        [NonAction]
        public virtual IActionResult Test()
        {
            return View();
        }

        #endregion

        #region Utilities
        protected void AddUpdateStock(int inventoryId, int qty)
        {
            var stock = _itemStockService.GetById(inventoryId);
            if (stock != null)
            {
                stock.Qty = stock.Qty - qty;
                stock.LastUpdate = DateTime.Now;
                _itemStockService.Update(stock);
            }

        }
        #endregion


    }
}
