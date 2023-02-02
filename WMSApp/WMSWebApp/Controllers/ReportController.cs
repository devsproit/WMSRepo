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
        #endregion

        #region Ctor
        public ReportController(IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IWarehouseService warehouseService, IBranchHelper branchHelper, IIntrasitService intrasitService, ISalePo salePo)
        {
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _warehouseService = warehouseService;
            _branchHelper = branchHelper;
            _intrasitService = intrasitService;
            _salePo = salePo;
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


                    return m;
                }),
                Total = data.TotalCount,
            };

            return Json(data);
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
        #endregion
    }
}
