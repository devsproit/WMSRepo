using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Application.Services.PO;
using WMS.Data;
using WMSWebApp.ViewModels.Invoice;
using System.Collections.Generic;
using Application.Services.Invoice;
using Domain.Model.Invoice;
using WMS.Web.Framework.Infrastructure.Extentsion;

namespace WMSWebApp.Controllers
{
    public class InvoiceController : BaseAdminController
    {

        #region Fields
        private readonly IPurchaseOrder _purchaseOrderService;
        private readonly ISalePo _salePoService;
        private readonly IServiceOrderPo _serviceOrderPoService;
        private readonly IStockTransferPo _stockTransferPoService;
        private readonly IWorkContext _workContext;
        private readonly IInvoiceService _invoiceService;
        #endregion

        #region Ctor
        public InvoiceController(IPurchaseOrder purchaseOrderService, ISalePo salePoService, IServiceOrderPo serviceOrderPoService, IStockTransferPo stockTransferPoService, IWorkContext workContext, IInvoiceService invoiceService)
        {
            _purchaseOrderService = purchaseOrderService;
            _salePoService = salePoService;
            _serviceOrderPoService = serviceOrderPoService;
            _stockTransferPoService = stockTransferPoService;
            _workContext = workContext;
            _invoiceService = invoiceService;
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
        public IActionResult List(DataSourceRequest request)
        {
            var data = _invoiceService.GetAllMaster(request.Page - 1, request.PageSize);
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
                    return m;

                }),
            };

            return Json(gridData);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(string id)
        {
            return Json("");
        }
        [HttpGet]
        public virtual IActionResult GetPoProduct(string id, string docType)
        {
            List<InvoiceItemModel> model = new List<InvoiceItemModel>();
            if (docType == "StockTransfer PO")
            {
                var items = _stockTransferPoService.GetStockTransferPos(id);
                foreach (var item in items)
                {
                    InvoiceItemModel m = new InvoiceItemModel()
                    {
                        Amount = 0,
                        SubItemName = item.StockTransferPOSubItem,
                        PoNumber = item.PONumber,
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
                    InvoiceItemModel m = new InvoiceItemModel()
                    {
                        Amount = Convert.ToInt32(item.SalePOAmt),
                        SubItemName = item.SalePOSubItem,
                        PoNumber = item.PONumber,
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
                    InvoiceItemModel m = new InvoiceItemModel()
                    {
                        Amount = item.ServiceOrderPOQty,
                        SubItemName = item.ServiceOrderPOSubitem,
                        PoNumber = item.PONumber,
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


        [HttpPost]
        public virtual IActionResult Save([FromBody] InvoiceModel model)
        {
            InvoiceMaster master = new InvoiceMaster();
            List<InvoiceDetails> details = new List<InvoiceDetails>();
            string docType = model.DocType;

            if (docType == "StockTransfer PO")
            {
                var items = _stockTransferPoService.GetStockTransferPos(model.PoNumber);
                master.PoNumber = model.PoNumber;
                master.PoCategory = docType;
                master.InvoiceNumber = model.InvoiceNo;
                master.BilledTo = items.FirstOrDefault().StockTransferPOSendingTo;
                var id = _invoiceService.InsertMaster(master);
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails()
                    {
                        Amt = 0,
                        SubItem = item.StockTransferPOSubItem,
                        PoCategory = docType,
                        InvoiceMasterId = id,
                        Qty = item.StockTransferPOQty,
                        SubItemCode = item.SubItemCode,
                        SerialNo = item.StockTransferPOSerialNumber


                    };
                    details.Add(m);
                }

                _invoiceService.InsertDetails(details);

            }
            else if (docType == "Sale PO")
            {
                var items = _salePoService.GetSalePos(model.PoNumber);
                master.PoNumber = model.PoNumber;
                master.PoCategory = docType;
                master.InvoiceNumber = model.InvoiceNo;
                master.BilledTo = items.FirstOrDefault().SalePOSendingTo;
                var id = _invoiceService.InsertMaster(master);
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails()
                    {
                        Amt = Convert.ToInt32(item.SalePOAmt),
                        SubItem = item.SalePOSubItem,
                        Qty = item.SalePOQty,
                        SubItemCode = item.SubItemCode,
                        InvoiceMasterId = id,
                        PoCategory = docType,
                        SerialNo = item.SalePOSerialNumber
                    };
                    details.Add(m);
                }
                _invoiceService.InsertDetails(details);
            }
            else

            {
                var items = _serviceOrderPoService.GetServicePos(model.PoNumber);
                master.PoNumber = model.PoNumber;
                master.PoCategory = docType;
                master.InvoiceNumber = model.InvoiceNo;
                master.BilledTo = items.FirstOrDefault().ServiceOrderPOSendingTo;
                var id = _invoiceService.InsertMaster(master);
                foreach (var item in items)
                {
                    InvoiceDetails m = new InvoiceDetails()
                    {
                        Amt = item.ServiceOrderPOQty,
                        SubItem = item.ServiceOrderPOSubitem,
                        Qty = item.ServiceOrderPOQty,
                        SubItemCode = item.SubItemCode,
                        SerialNo = item.ServiceOrderPOSerialNumber,
                        InvoiceMasterId = id,
                        PoCategory = docType,


                    };
                    details.Add(m);
                }

                _invoiceService.InsertDetails(details);
            }

            return Json(true);
        }
        #endregion

    }
}
