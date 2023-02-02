using Application.Services.Invoice;
using Application.Services.Master;
using Domain.Model.Invoice;
using Domain.Model.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSAPI.ViewModels.Dispatch;
using WMSAPI.ViewModels.Invoice;

namespace WMSAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Authorize]
    public class DispatchController : BaseAdminController
    {

        #region Fields
        private readonly IInvoiceService _invoiceService;
        private readonly IDispatchService _dispatchService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor
        public DispatchController(IInvoiceService invoiceService, IDispatchService dispatchService, IWorkContext workContext)
        {
            _invoiceService = invoiceService;
            _dispatchService = dispatchService;
            _workContext = workContext;
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

        [NonAction]
        public async Task<IActionResult> Create()
        {
            DispatchModel model = new DispatchModel();
            var branch = await _workContext.GetCurrentBranch();
            var invoice = _invoiceService.GetAllMaster(branch.BranchCode, 0, int.MaxValue, "NDP");
            model.InvoiceList = invoice.ToList();
            return View(model);
        }
        [NonAction]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DispatchModel model)
        {
            var branch = await _workContext.GetCurrentBranch();
            Dispatch dispatch = new Dispatch();
            dispatch.DispatchDate = model.DispatchDate;
            dispatch.CreateOn = DateTime.Now;
            dispatch.InvoiceId = model.InvoiceId;
            dispatch.PO = model.PO;
            dispatch.VendorName = model.VendorName;
            dispatch.VehicleNumber = model.VehicleNumber;
            dispatch.Location = model.Location;
            dispatch.BranchCode = branch.BranchCode;
            dispatch.DocketNo = model.DocketNo;
            dispatch.LRNo = model.LRNo;
            _dispatchService.Insert(dispatch);
            var invoice = _invoiceService.GetById(model.InvoiceId);
            invoice.DispatchDone = true;
            _invoiceService.Update(invoice);

            SuccessNotification("Dispatch created successfully.");
            return RedirectToAction("List");
        }
        [NonAction]
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
