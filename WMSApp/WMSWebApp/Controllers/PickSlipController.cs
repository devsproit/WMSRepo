using Microsoft.AspNetCore.Mvc;
using Application.Services.PS;
using Application.Services.GRN;
using Microsoft.AspNetCore.Authorization;
using Domain.Model.PS;
using WMS.Web.Framework.Infrastructure.Extentsion;
using System;
using System.Linq;
using Application.Services.WarehouseMaster;
using WMSWebApp.ViewModels.Pickslip;
using WMS.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using WMSWebApp.ViewModels.GRN;
using Application.Services.PO;
namespace WMSWebApp.Controllers
{
    [Authorize]
    public class PickSlipController : BaseAdminController
    {

        #region Fields
        private readonly ITempPickSlipDetailsService _tempPickSlipDetailsService;
        private readonly IPickSlipService _pickSlipService;
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IWorkContext _workContext;

        private readonly IPurchaseOrder _purchaseOrder;
        private readonly ISalePo _salePoService;
        private readonly IServiceOrderPo _serviceOrderPoService;
        private readonly IStockTransferPo _stockTransferPoService;


        #endregion

        #region Ctor
        public PickSlipController(ITempPickSlipDetailsService tempPickSlipDetailsService, IPickSlipService pickSlipService, IWarehouseService warehouseService, IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IWorkContext workContext, IPurchaseOrder purchaseOrder, ISalePo salePoService, IServiceOrderPo serviceOrderPoService, IStockTransferPo stockTransferPoService)
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
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }


        public virtual IActionResult List(DataSourceRequest request, string guid)
        {
            var result = _tempPickSlipDetailsService.GetAllTemp(guid, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = result.Select(x =>
                {
                    PickslipDetailModel m = new PickslipDetailModel();
                    m.Qty = x.Qty;
                    //m.Unit = x.Unit;
                    m.PickSlipId = x.PickSlipId;
                    //m.Amount = x.Amount;
                    m.AreaId = x.AreaId;
                    var area = _warehouseService.GetWarehouseZoneAreaById(x.AreaId);
                    if (area != null)
                    {
                        var zone = _warehouseService.GetZoneById(area.ZoneId);
                        m.Location = $"Zone-{zone.ZoneName} Area- {area.AreaName}";
                    }
                    m.ItemCode = x.ItemCode;
                    m.SubItemName = x.SubItemName;
                    m.SubItemCode = x.SubItemCode;
                    m.Guid = x.Guid;
                    m.Id = x.Id;
                    m.GRN = x.GRNId;
                    return m;
                }),

                Total = result.TotalCount
            };

            return Json(gridData);
        }

        [HttpGet]
        public virtual IActionResult GrnList()
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var pos = _goodReceivedNoteMasterService.GetAllMaster(branch.BranchCode).ToList();
            List<GrnListModel> list = new List<GrnListModel>();
            list = pos.Select(x => new GrnListModel() { GRNId = x.Id }).ToList();
            return Json(list);
        }


        [HttpGet]
        public virtual IActionResult PoList(string docType)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var pos = _purchaseOrder.GetPurchaseOrders(branch.BranchCode, docType);
            List<GrnListModel> list = new List<GrnListModel>();
            list = pos.Select(x => new GrnListModel() { PoNumber = x.PONumber }).ToList();
            return Json(list);
        }

        [HttpGet]
        public virtual IActionResult GetGrnProduct(int id)
        {
            var grnitems = _goodReceivedNoteMasterService.GetbyId(id);
            var items = grnitems.GoodReceivedNoteDetails.ToList();
            List<GrnItemListModel> model = new List<GrnItemListModel>();
            foreach (var item in items)
            {
                GrnItemListModel m = new GrnItemListModel()
                {
                    Amount = item.Amount,
                    SubItemName = item.SubItemName,
                    AreaId = item.AreaId,
                    GRNId = item.GRNId,
                    Id = item.Id,
                    ItemCode = item.ItemCode,
                    MaterialDescription = item.MaterialDescription,
                    Qty = item.Qty,
                    SubItemCode = item.SubItemCode,
                    Unit = item.Unit,
                };
                model.Add(m);
            }
            return Json(model);
        }


        [HttpGet]
        public virtual IActionResult GetPoProduct(string id, string docType)
        {
            List<GrnItemListModel> model = new List<GrnItemListModel>();
            if (docType == "StockTransfer PO")
            {
                var items = _stockTransferPoService.GetStockTransferPos(id);
                foreach (var item in items)
                {
                    GrnItemListModel m = new GrnItemListModel()
                    {
                        Amount = 0,
                        SubItemName = item.StockTransferPOSubItem,
                        GRNId = item.Id,
                        Id = item.Id,
                        ItemCode = item.StockTransferPOItem,
                        MaterialDescription = "",
                        Qty = item.StockTransferPOQty,
                        SubItemCode = item.SubItemCode,

                    };
                    model.Add(m);
                }
            }
            else if (docType == "Sale PO")
            {
                var items = _salePoService.GetSalePos(id);
                foreach (var item in items)
                {
                    GrnItemListModel m = new GrnItemListModel()
                    {
                        Amount = Convert.ToInt32(item.SalePOAmt),
                        SubItemName = item.SalePOSubItem,
                        //AreaId = item.AreaId,
                        //GRNId = item.GRNId,
                        Id = item.Id,
                        ItemCode = item.SalePOSubItem,
                        MaterialDescription = "",
                        Qty = item.SalePOQty,
                        SubItemCode = item.SubItemCode,

                    };
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

                    };
                    model.Add(m);
                }
            }
            return Json(model);

        }
        [HttpGet]
        public virtual IActionResult GetLocation(int areaId)
        {
            string location = "";
            var area = _warehouseService.GetWarehouseZoneAreaById(areaId);
            if (area != null)
            {
                var zone = _warehouseService.GetZoneById(area.ZoneId);
                location = $"Zone-{zone.ZoneName} Area- {area.AreaName}";
            }
            return Json(location);
        }

        [HttpPost]
        public virtual IActionResult Save(PickslipDetailModel model)
        {

            return Json(true);
        }
        #endregion


    }
}
