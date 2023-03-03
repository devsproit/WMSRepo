using Microsoft.AspNetCore.Mvc;
using WMS.Web.Framework.Infrastructure.Extentsion;
using System;
using Application.Services.GRN;
using System.Linq;
using WMSWebApp.ViewModels.Report;
using Application.Services.WarehouseMaster;
using Application.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using WMSWebApp.ViewModels;
using Application.Services.PO;
using WMSWebApp.ViewModels.PO;
using Sentry;
using WMS.Data;
using Application.Services.PS;
using DocumentFormat.OpenXml.Office2016.Excel;
using Domain.Model;
using Application.Services.Master;
using WMSWebApp.ViewModels.Dispatch;
using Application.Services.Invoice;
using System.Threading.Tasks;

namespace WMSWebApp.Controllers
{
    public class ReportController : BaseAdminController
    {
        #region Fields
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IWarehouseService _warehouseService;
        private readonly IBranchHelper _branchHelper;
        private readonly IIntrasitService _intrasitService;
        private readonly ISalePo _salePo;
        private readonly IStockTransferPo _stockTransferPo;
        private readonly IServiceOrderPo _serviceOrderPo;
        private readonly ISrnPo _srnPo;
        private readonly IPickSlipService _pickSlipService;
        private readonly IDispatchService _dispatchService;
        private readonly IInvoiceService _invoiceService;
        #endregion

        #region Ctor
        public ReportController(IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IWarehouseService warehouseService, IBranchHelper branchHelper, IIntrasitService intrasitService, ISalePo salePo, IStockTransferPo stockTransferPo, IServiceOrderPo serviceOrderPo, ISrnPo srnPo, IPickSlipService pickSlipService, IDispatchService dispatchService, IInvoiceService invoiceService)
        {
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _warehouseService = warehouseService;
            _branchHelper = branchHelper;
            _intrasitService = intrasitService;
            _salePo = salePo;
            _stockTransferPo = stockTransferPo;
            _serviceOrderPo = serviceOrderPo;
            _srnPo = srnPo;
            _pickSlipService = pickSlipService;
            _dispatchService = dispatchService;
            _invoiceService = invoiceService;
        }
        #endregion

        #region Methods
        public virtual IActionResult GRNReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public virtual IActionResult GRNReport(DataSourceRequest request, DateTime fromdate, DateTime to, string branchCode)
        {
            var data = _goodReceivedNoteMasterService.GetReport(fromdate, to, branchCode, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = data.Select(x =>
                {
                    GRNDataModel m = new GRNDataModel();
                    m.Id = x.Id;

                    m.PONo = x.GoodReceivedNoteMaster.PONo;
                    m.InvoiceNo = x.GoodReceivedNoteMaster.InvoiceNo;
                    m.InvoiceDate = x.GoodReceivedNoteMaster.InvoiceDate;
                    m.BranchCode = x.GoodReceivedNoteMaster.BranchCode;
                    m.SenderCompany = x.GoodReceivedNoteMaster.SenderCompany;
                    m.ItemCode = x.ItemCode;
                    m.SubItemCode = x.SubItemCode;
                    m.SubItemName = x.SubItemName;
                    m.MaterialDescription = x.MaterialDescription;
                    m.Qty = x.Qty;
                    m.Unit = x.Unit;
                    m.Amount = x.Amount;
                    var area = _warehouseService.GetWarehouseZoneAreaById(x.AreaId);
                    m.Area = area.AreaName;
                    m.IRN = x.GoodReceivedNoteMaster.IRN;
                    m.SAPNO = x.GoodReceivedNoteMaster.GRNNumberOfSAP;

                    return m;
                }),
                Total = data.TotalCount,
            };

            return Json(gridData);
        }


        public virtual IActionResult IntrasitReport()
        {
            return View();
        }

        public virtual IActionResult IntrasitDone()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public virtual IActionResult IntrasitDone(DataSourceRequest request, string branchCode)
        {
            var intrasitData = _intrasitService.GetDonePO(branchCode, request.Page - 1, request.PageSize);
            int id = 1;
            var data = new DataSourceResult()
            {
                Data = intrasitData.Select(x =>
                {
                    var m = new Intrasitc();

                    m.Id = x.Id;
                    m.LoginBranch = x.Login_Branch;
                    m.SenderCompany = x.Sender_Company;
                    m.Branch = x.Sender_Branch;
                    m.PurchaseOrder = x.PurchaseOrder;
                    m.ItemCode = x.Item_Code;
                    m.SubItemCode = x.SubItem_Code;
                    m.SubItemName = x.SubItem_Name;
                    m.MaterialDescription = x.Material_Description;
                    m.Qty = x.Qty;
                    m.Unit = x.Unit;
                    m.Amt = x.Amt;
                    m.ETA = x.ETA;
                    m.Sno = id;
                    m.AllowGRN = false;
                    id++;
                    return m;
                }),
                Total = intrasitData.TotalCount
            };

            return Json(data);
        }


        public virtual IActionResult SalePoReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }


        [HttpPost]
        public virtual IActionResult SalePoReport(DataSourceRequest request, string branchCode, string status)
        {
            var result1 = _salePo.GetAllSalePo(status, branchCode, request.Page - 1, request.PageSize);
            var gridData1 = new DataSourceResult()
            {
                Data = result1.Select(x =>
                {
                    PolistViewModel m = new PolistViewModel();
                    m.Id = x.Id;
                    m.PoNumber = x.PONumber;
                    m.stockTransferPOCatagry = x.SalePOCategory;
                    m.stockTransferPoSendingTo = x.SalePOSendingTo;
                    m.stockTransferPoItem = x.SalePOItem;
                    m.stockTransferPoSubitem = x.SalePOSubItem;
                    m.stockTransferPoQty = x.SalePOQty.ToString();
                    m.stockTransferPoAmt = x.SalePOAmt;
                    m.stockTransferPoSerialNumber = x.SalePOSerialNumber;
                    m.serviceCategory = "Not Applicable";
                    m.salePo = "Not Applicable";
                    m.saleDate = "Not Applicable";
                    m.ServiceRequestNumber = "Not Applicable";
                    m.subItemCode = x.SubItemCode;
                    m.invNumber = "";
                    return m;
                }),
                Total = result1.TotalCount
            };
            return Json(gridData1);
        }

        public virtual IActionResult StockTransferReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public virtual IActionResult StockTransferReport(DataSourceRequest request, string branchCode, string status)
        {
            var result = _stockTransferPo.GetAllStockTrasnferPo(status, branchCode, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = result.Select(x =>
                {
                    PolistViewModel m = new PolistViewModel();
                    m.Id = x.Id;
                    m.PoNumber = x.PONumber;
                    m.stockTransferPOCatagry = x.StockTransferPOCategory;
                    m.stockTransferPoSendingTo = x.StockTransferPOSendingTo;
                    m.stockTransferPoItem = x.StockTransferPOItem;
                    m.stockTransferPoSubitem = x.StockTransferPOSubItem;
                    m.stockTransferPoQty = x.StockTransferPOQty.ToString();
                    m.stockTransferPoAmt = x.StockTransferPOAmt;
                    m.stockTransferPoSerialNumber = x.StockTransferPOSerialNumber;
                    m.serviceCategory = "Not Applicable";
                    m.salePo = "Not Applicable";
                    m.saleDate = "Not Applicable";
                    m.ServiceRequestNumber = "Not Applicable";
                    m.subItemCode = x.SubItemCode;
                    m.invNumber = "";
                    return m;
                }),

                Total = result.TotalCount
            };
            return Json(gridData);
        }


        public virtual IActionResult ServiceOrderPOReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public virtual IActionResult ServiceOrderPOReport(DataSourceRequest request, string status, string branchCode)
        {
            var result3 = _serviceOrderPo.GetAllServiceOrderPo(status, branchCode, request.Page - 1, request.PageSize);
            var gridData3 = new DataSourceResult()
            {
                Data = result3.Select(x =>
                {
                    PolistViewModel m = new PolistViewModel();
                    m.Id = x.Id;
                    m.PoNumber = x.PONumber;
                    m.stockTransferPOCatagry = x.ServiceOrderPOCategory;
                    m.stockTransferPoSendingTo = x.ServiceOrderPOSendingTo;
                    m.stockTransferPoItem = x.ServiceOrderPOSubitem;
                    m.stockTransferPoSubitem = x.ServiceOrderPOSubitem;
                    m.stockTransferPoQty = x.ServiceOrderPOQty.ToString();
                    m.stockTransferPoAmt = x.ServiceOrderPOAmt;
                    m.stockTransferPoSerialNumber = x.ServiceOrderPOSerialNumber;
                    m.serviceCategory = x.ServiceOrderPOServiceCatagry;
                    m.salePo = "Not Applicable";
                    m.saleDate = "Not Applicable";
                    m.ServiceRequestNumber = x.ServiceOrderPOServiceRequestNumber;
                    m.subItemCode = x.SubItemCode;
                    m.invNumber = "";
                    return m;
                }),
                Total = result3.TotalCount
            };
            return Json(gridData3);
        }

        public virtual IActionResult SRNReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public virtual IActionResult SRNReport(DataSourceRequest request, string status, string branchCode)
        {
            var result2 = _srnPo.GetAllSRNPo(status, branchCode, request.Page - 1, request.PageSize);
            var gridData2 = new DataSourceResult()
            {
                Data = result2.Select(x =>
                {
                    PolistViewModel m = new PolistViewModel();
                    m.Id = x.Id;
                    m.PoNumber = x.PONumber;
                    m.stockTransferPOCatagry = x.SrnPOCategory;
                    m.stockTransferPoSendingTo = x.SrnPOSendingTo;
                    m.stockTransferPoItem = x.SrnPOItem;
                    m.stockTransferPoSubitem = x.SrnPOSubItem;
                    m.stockTransferPoQty = x.SrnPOQty.ToString();
                    m.stockTransferPoAmt = "0";
                    m.stockTransferPoSerialNumber = x.SrnSerialNumber;
                    m.serviceCategory = "Not Applicable";
                    m.salePo = "Not Applicable";
                    m.saleDate = "Not Applicable";
                    m.ServiceRequestNumber = "Not Applicable";
                    m.subItemCode = x.SubItemCode;
                    m.invNumber = x.InvoiceNo;
                    return m;
                }),
                Total = result2.TotalCount
            };
            return Json(gridData2);
        }

        public virtual IActionResult PickSlipReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());

        }

        [HttpPost]
        public IActionResult PickSlipReport(DataSourceRequest request, string branchCode, string status)
        {

            var result = _pickSlipService.GetAllPickSlip(branchCode, status, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = result.Select(x =>
                {
                    PickSlipModel m = new PickSlipModel();
                    m.PickSlipName = x.PickSlipMaster.PickSlipName;
                    m.BranchCode = x.PickSlipMaster.BranchCode;
                    m.Branch = x.PickSlipMaster.BranchCode;
                    m.DockType = x.PickSlipMaster.DockType;
                    m.CreateOn = x.PickSlipMaster.CreateOn;
                    m.Id = x.Id;
                    m.Item = x.PickSlipMaster.PickSlipDetails.Count;
                    if (x.AreaId > 0)
                    {
                        var area = _warehouseService.GetWarehouseZoneAreaById(x.AreaId);
                        m.AreaName = area.AreaName;
                    }

                    m.SubItemName = x.SubItemName;
                    m.ItemCode = x.ItemCode;
                    m.SubItemCode = x.SubItemCode;
                    m.Qty = x.Qty;
                    m.Unit = x.Unit;
                    m.InventoryId = x.InventoryId;


                    return m;
                }),

                Total = result.TotalCount,
            };

            return Json(gridData);
        }

        //public virtual IActionResult DispatchReport()
        //{
        //    var data = _invoiceService.GetAllMaster(branch.BranchCode, request.Page - 1, request.PageSize, "ALL");
        //    DataSourceResult gridData = new DataSourceResult
        //    {
        //        Data = data.Select(x =>
        //        {
        //            InvoiceListModel m = new InvoiceListModel();
        //            m.PoNumber = x.PoNumber;
        //            m.PoCategory = x.PoCategory;
        //            m.CreateOn = x.CreateOn;
        //            m.InvoiceNumber = x.PoNumber;
        //            m.BilledTo = x.BilledTo;
        //            m.Id = x.Id;
        //            return m;

        //        }),
        //        Total = data.TotalCount,
        //    };

        //    return Json(gridData);
        //}

        public IActionResult DispatchReport()
        {
            var branch = _branchHelper.GetAllBranches();
            return View(branch.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> DispatchReport(DataSourceRequest request, string branchCode, DateTime fromdate, DateTime todate)
        {

            var dispatches = _dispatchService.GetAllDispatchReport(branchCode, fromdate, todate, request.Page - 1, request.PageSize);
            DataSourceResult dataSource = new DataSourceResult
            {
                Data = dispatches.Select(x =>
                {
                    DispatchList m = new DispatchList();
                    m.DispatchDate = x.DispatchDate;
                    m.CreateOn = DateTime.Now;

                    m.PO = x.PO;
                    m.VendorName = x.VendorName;
                    m.VehicleNumber = x.VehicleNumber;
                    m.Location = x.Location;
                    m.Id = x.Id;
                    m.BranchCode = x.BranchCode;
                    var invoice = _invoiceService.GetById(x.InvoiceId);
                    m.InvoiceDate = invoice.CreateOn;
                    m.InvoiceId = x.InvoiceId;
                    m.Invoice = invoice.InvoiceNumber;
                    m.LRN0 = x.LRNo;
                    m.BillTo = invoice.BilledTo;
                    m.Qty = invoice.InvoiceDetails.Sum(x => x.Qty);
                    return m;
                }),
                Total = dispatches.TotalCount
            };
            return Json(dataSource);
        }
        #endregion
    }
}
