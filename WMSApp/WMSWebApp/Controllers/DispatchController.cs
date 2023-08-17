using Application.Services.Invoice;
using Application.Services.Master;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Model.Invoice;
using Domain.Model.Masters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels.Dispatch;
using WMSWebApp.ViewModels.Invoice;

namespace WMSWebApp.Controllers
{
    public class DispatchController : BaseAdminController
    {

        #region Fields
        private readonly IInvoiceService _invoiceService;
        private readonly IDispatchService _dispatchService;
        private readonly IWorkContext _workContext;
        private readonly ILogger<DispatchController> _logger;
        IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
        #endregion

        #region Ctor
        public DispatchController(IInvoiceService invoiceService, IDispatchService dispatchService, IWorkContext workContext, ILogger<DispatchController> logger)
        {
            _invoiceService = invoiceService;
            _dispatchService = dispatchService;
            _workContext = workContext;
            _logger = logger;
        }
        #endregion

        #region Methods

        public IActionResult Index()
        {
            return RedirectToAction("List");

        }

        public IActionResult List()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest request)
        {
            var branch = await _workContext.GetCurrentBranch();
            var dispatches = _dispatchService.GetAllDispatch(branch.BranchCode, 0, int.MaxValue);
            DataSourceResult dataSource = new DataSourceResult
            {
                Data = dispatches.Select(x =>
                {
                    DispatchList m = new DispatchList();
                    m.DispatchDate = x.DispatchDate;
                    m.CreateOn = DateTime.Now;
                    m.InvoiceId = x.InvoiceId;
                    m.PO = x.PO;
                    m.VendorName = x.VendorName;
                    m.VehicleNumber = x.VehicleNumber;
                    m.Location = x.Location;
                    m.Id = x.Id;
                    m.BranchCode = x.BranchCode;
                    var invoice = _invoiceService.GetById(x.InvoiceId);
                    m.InvoiceDate = invoice.CreateOn;
                    return m;
                }),
                Total = dispatches.TotalCount
            };
            return Json(dataSource);
        }

        public async Task<IActionResult> Create()
        {
            DispatchModel model = new DispatchModel();
            var branch = await _workContext.GetCurrentBranch();
            var invoice = _invoiceService.GetAllMaster(branch.BranchCode, 0, int.MaxValue, "NDP");
            model.InvoiceList = invoice.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DispatchModel model)
        {
            var branch = await _workContext.GetCurrentBranch();
            Dispatch dispatch = new Dispatch();

            dispatch.DispatchDate = Convert.ToDateTime(model.DispatchDate, culture);
            dispatch.CreateOn = DateTime.Now;
            dispatch.InvoiceId = model.InvoiceId;
            dispatch.PO = model.PO;
            dispatch.VendorName = model.VendorName;
            dispatch.VehicleNumber = model.VehicleNumber;
            dispatch.Location = model.Location;
            dispatch.BranchCode = branch.BranchCode;
            dispatch.DocketNo = model.DocketNo;
            dispatch.LRNo = model.LRNo;
            //string json = JsonConvert.SerializeObject(dispatch);
            //SentrySdk.CaptureMessage(json);
            _dispatchService.Insert(dispatch);
            var invoice = _invoiceService.GetById(model.InvoiceId);
            invoice.DispatchDone = true;
            _invoiceService.Update(invoice);

            SuccessNotification("Dispatch created successfully.");
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _invoiceService.GetById(id);
            List<ItemModel> items = new List<ItemModel>();
            foreach (var item in invoice.InvoiceDetails)
            {
                ItemModel details = new ItemModel();
                details.Amt = item.Amt;
                details.AreaCode = item.AreaCode;
                details.AreaId = item.AreaId;
                details.AreaName = item.AreaName;
                details.Id = item.Id;
                details.InvoiceMasterId = item.InvoiceMasterId;
                details.MaterialDescription = item.MaterialDescription;
                details.PoCategory = item.PoCategory;
                details.Qty = item.Qty;
                details.SerialNo = item.SerialNo;
                details.SubItem = item.SubItem;
                details.SubItemCode = item.SubItemCode;
                details.SubItemName = item.SubItemName;
                details.WarehouseCode = item.WarehouseCode;
                details.Warehouse = item.Warehouse;
                details.ZoneCode = item.ZoneCode;
                details.ZoneName = item.ZoneName;
                details.PONumber = invoice.PoNumber;
                items.Add(details);
            }
            return Json(items);

        }


        #endregion



    }
}
