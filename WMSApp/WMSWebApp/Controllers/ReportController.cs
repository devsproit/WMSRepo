using Microsoft.AspNetCore.Mvc;
using WMS.Web.Framework.Infrastructure.Extentsion;
using System;
using Application.Services.GRN;
using System.Linq;
using WMSWebApp.ViewModels.Report;
using Application.Services.WarehouseMaster;
using Application.Services;
namespace WMSWebApp.Controllers
{
    public class ReportController : BaseAdminController
    {
        #region Fields
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IWarehouseService _warehouseService;
        private readonly IBranchHelper _branchHelper;
        #endregion

        #region Ctor
        public ReportController(IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IWarehouseService warehouseService, IBranchHelper branchHelper)
        {
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _warehouseService = warehouseService;
            _branchHelper = branchHelper;
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

        #endregion
    }
}
