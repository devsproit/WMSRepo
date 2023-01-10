using Application.Services;
using Application.Services.PO;
using AutoMapper;
using Domain.Model;
using Domain.Model.PO;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels.PO;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class PoController : BaseAdminController
    {

        #region Fields

        private readonly IMapper _mapper;
        private readonly IPurchaseOrder _purchaseOrder;
        private readonly ISubItemHelper _SubItemHelper;
        private readonly IBranchHelper _BranchHelper;
        private readonly IItemHelper _ItemHelper;
        private readonly ISenderCompany _senderCompany;
        private readonly ISalePo _salePo;
        private readonly IStockTransferPo _stockTransferPo;
        private readonly IServiceOrderPo _serviceOrderPO;
        private readonly ISrnPo _srnPo;
        private readonly ICustomerHelper _customerHelper;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public PoController(IMapper mapper, IPurchaseOrder purchaseOrder, ISubItemHelper SubItemHelper, IBranchHelper BranchHelper, IItemHelper ItemHelper, ISenderCompany senderCompany,
            ISalePo salePo, IStockTransferPo stockTransferPo, ISrnPo srnPo, ICustomerHelper customerHelper, IServiceOrderPo serviceOrderPo, IWorkContext workContext)
        {
            _SubItemHelper = SubItemHelper;
            _mapper = mapper;
            _purchaseOrder = purchaseOrder;
            _BranchHelper = BranchHelper;
            _ItemHelper = ItemHelper;
            _senderCompany = senderCompany;
            _salePo = salePo;
            _stockTransferPo = stockTransferPo;
            _serviceOrderPO = serviceOrderPo;
            _srnPo = srnPo;
            _customerHelper = customerHelper;
            _workContext = workContext;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest request, string category)
        {
            switch (category)
            {

                case "StockTransfer PO":
                    var result = _stockTransferPo.GetDetails(category, request.Page - 1, request.PageSize);
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
                case "Sale PO":
                    var result1 = _salePo.GetDetails(category, request.Page - 1, request.PageSize);
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
                case "SRN PO":
                    var result2 = _srnPo.GetDetails(category, request.Page - 1, request.PageSize);
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
                case "ServiceOrder PO":
                    var result3 = _serviceOrderPO.GetDetails(category, request.Page - 1, request.PageSize);
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
            return Json("");
        }

        public ActionResult Create()
        {
            PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel();
            var listSubItem = _SubItemHelper.GetAllSubItem();
            var listBranch = _BranchHelper.GetAllBranch();
            var listItem = _ItemHelper.GetAllItem();
            var listSendCompany = _senderCompany.GetAllSenderCompanies();
            //  var listCategory = "";
            listSendCompany.Insert(0, new SenderCompanyNameDb { Id = 0, Sender_Company_Name = "Select" });
            listBranch.Insert(0, new Branch { Id = 0, BranchName = "Select" });
            listItem.Insert(0, new ItemDb { Id = 0, ItemName = "Select" });
           // listSubItem.Insert(0, new SubItemDb { Id = 0, SubItemName = "Select" });
            ViewBag.ListofSenderCompany = listSendCompany.ToList();
            ViewBag.listBranch = listBranch.ToList();
            ViewBag.listItem = listItem.ToList();
            ViewBag.listSubItem = listSubItem.ToList();

            purchaseOrderViewModel.listItem = listItem;
            purchaseOrderViewModel.listSubItem = listSubItem;
            purchaseOrderViewModel.listBranch = listBranch;
            purchaseOrderViewModel.listSenderCompany = listSendCompany;
            return View();
        }

        [HttpGet]
        public virtual async Task<IActionResult> SearchSubItem()
        {
            var subItemsData = _SubItemHelper.GetSubItem("0", 0, int.MaxValue).ToList().GetUniqeCode();
            //subItems = _mapper.Map<List<SubItem>>(subItemsData);
            return Json(subItemsData);
        }


        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] PoViewModel poViewModel)
        {
            try
            {
                var branch = await _workContext.GetCurrentBranch();
                DateTime currentDate = DateTime.Now;
                PurchaseOrderDb purchaseOrderDb = new PurchaseOrderDb();
                purchaseOrderDb.POCategory = poViewModel.POCatrgory;
                purchaseOrderDb.PODate = poViewModel.PODate;
                purchaseOrderDb.PONumber = poViewModel.PONumber;
                purchaseOrderDb.BranchCode = branch.BranchCode;
                purchaseOrderDb.ProcessStatus = false;
                if (poViewModel.stockTransferCategories != null)
                {
                    _purchaseOrder.Insert(purchaseOrderDb);
                    foreach (var item in poViewModel.stockTransferCategories)
                    {

                        StockTransferPoDb stockTransferPo = new StockTransferPoDb();
                        stockTransferPo.StockTransferPOCategory = item.StockTransferPOCategory;
                        stockTransferPo.StockTransferPOSendingTo = item.StockTransferPOSendingTo;
                        stockTransferPo.StockTransferPOItem = item.StockTransferPOItem;
                        stockTransferPo.StockTransferPOQty = item.StockTransferPOQty;
                        stockTransferPo.StockTransferPOAmt = item.StockTransferPOAmt;
                        stockTransferPo.StockTransferPOSerialNumber = item.StockTransferPOSerialNumber;
                        stockTransferPo.PONumber = poViewModel.PONumber;
                        stockTransferPo.IsProcessed = false;
                        var subItem = _SubItemHelper.GetItemByCOde(item.SubItemCode);
                        stockTransferPo.SubItemCode = subItem.SubItemCode;
                        stockTransferPo.StockTransferPOSubItem = subItem.SubItemName;
                        _stockTransferPo.Insert(stockTransferPo);
                    }
                }
                else if (poViewModel.serviceOrderCategories != null)
                {
                    _purchaseOrder.Insert(purchaseOrderDb);
                    foreach (var item in poViewModel.serviceOrderCategories)
                    {
                        ServiceOrderPODb serviceOrderPODb = new ServiceOrderPODb();
                        serviceOrderPODb.ServiceOrderPOCategory = item.ServiceOrderPOCategory;
                        serviceOrderPODb.ServiceOrderPOSendingTo = item.ServiceOrderPOSendingTo;
                        serviceOrderPODb.ServiceOrderPOItem = item.ServiceOrderPOItem;
                        
                        serviceOrderPODb.ServiceOrderPOQty = item.ServiceOrderPOQty;
                        serviceOrderPODb.ServiceOrderPOAmt = item.ServiceOrderPOAmt;
                        serviceOrderPODb.ServiceOrderPOSerialNumber = item.ServiceOrderPOSerialNumber;
                        serviceOrderPODb.PONumber = poViewModel.PONumber;
                        serviceOrderPODb.ServiceOrderPOServiceCatagry = item.ServiceOrderPOServiceCatagry;
                        serviceOrderPODb.ServiceOrderPOServiceRequestNumber = item.ServiceOrderPOServiceRequestNumber;
                        serviceOrderPODb.ServiceOrderPOSalePO = item.ServiceOrderPOSalePO;
                        serviceOrderPODb.ServiceOrderPOSaleDate = item.ServiceOrderPOSaleDate;
                        serviceOrderPODb.IsProcessed = false;
                        var subItem = _SubItemHelper.GetItemByCOde(item.SubItemCode);
                        serviceOrderPODb.SubItemCode = subItem.SubItemCode;
                        serviceOrderPODb.ServiceOrderPOSubitem = subItem.SubItemName;
                        _serviceOrderPO.Insert(serviceOrderPODb);
                    }

                }
                else if (poViewModel.salePOCategories != null)
                {
                    _purchaseOrder.Insert(purchaseOrderDb);
                    foreach (var item in poViewModel.salePOCategories)
                    {
                        SalePoDb salePoDb = new SalePoDb();
                        salePoDb.SalePOCategory = item.SalePOCategory;
                        salePoDb.SalePOSendingTo = item.SalePOSendingTo;
                        salePoDb.SalePOItem = item.SalePOItem;
                        salePoDb.SalePOQty = item.SalePOQty;
                        salePoDb.SalePOAmt = item.SalePOAmt;
                        salePoDb.SalePOSerialNumber = item.SalePOSerialNumber;
                        salePoDb.PONumber = poViewModel.PONumber;
                        salePoDb.IsProcessed = false;
                        var subItem = _SubItemHelper.GetItemByCOde(item.SubItemCode);
                        salePoDb.SubItemCode = subItem.SubItemCode;
                        salePoDb.SalePOSubItem = subItem.SubItemName;
                        _salePo.Insert(salePoDb);
                    }
                }
                else if (poViewModel.sRNPOCategories != null)
                {
                    _purchaseOrder.Insert(purchaseOrderDb);
                    foreach (var item in poViewModel.sRNPOCategories)
                    {
                        SRNPoDb sRNPo = new SRNPoDb();
                        sRNPo.SrnPOSrnCause = item.SrnPOSrnCause;
                        sRNPo.SrnSerialNumber = item.SrnSerialNumber;
                        sRNPo.SrnPOCategory = item.SrnPOCategory;
                        sRNPo.SrnPOQty = item.SrnPOQty;
                        sRNPo.SrnPOItem = item.SrnPOItem;
                        sRNPo.SrnPOSendingTo = item.SrnPOSendingTo;
                        sRNPo.PONumber = poViewModel.PONumber;
                        sRNPo.InvoiceNo = item.InvoiceNo;
                        sRNPo.IsProcess = false;
                        var subItem = _SubItemHelper.GetItemByCOde(item.SubItemCode);
                        sRNPo.SubItemCode = subItem.SubItemCode;
                        sRNPo.SrnPOSubItem = subItem.SubItemName;
                        _srnPo.Insert(sRNPo);
                    }

                }

                return Json(new { success = true, message = "Saved Successfully" });
            }
            catch (Exception ex)
            {
                throw (ex);
                return Json(new { success = false, message = "Not Saved Successfully" });

            }

        }
        ///<summary>
        ///Get Sendgin to Based on Sale_PO, SRN_PO,ServiceOrder_PO
        /// </summary>
        /// <param name="category">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        public JsonResult GetSendingTo(string category)
        {
            var data = _customerHelper.GetCustomerByName(category);
            return Json(data);
        }

        ///<summary>
        ///Get Sendgin to Based on Sale_PO, SRN_PO,ServiceOrder_PO
        /// </summary>
        /// <param name="category">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        public JsonResult GetAmtTo(string subName, string type)
        {
            var data = _SubItemHelper.GetSubItemCustomerAmt(subName, type);
            return Json(data);
        }
        #endregion

        #region  bulk upload
        [HttpPost]
        public IActionResult OnPostMyUploader(IFormFile importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {

                //Getting FileName
                var fileName = Path.GetFileName(importFile.FileName);
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                DataSet ds = new DataSet();
                using (var target = new MemoryStream())
                {
                    importFile.CopyTo(target);
                    using (var reader = ExcelReaderFactory.CreateReader(target))
                    {
                        ds = reader.AsDataSet();
                    }
                    //var fileData = GetDataFromCSVFile(ds);
                    GetDataFromCSVFile(ds);
                }


                //var dtEmployee = fileData.ToDataTable();
                //var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured)
                //{
                //    TypeName = "dbo.tblTypeEmployee",
                //    Value = dtEmployee
                //};
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }
        private void GetDataFromCSVFile(DataSet ds)
        {

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PoNumber", typeof(string));
                dt.Columns.Add("POCatagry", typeof(string));
                dt.Columns.Add("SendingTo", typeof(string));
                dt.Columns.Add("Item", typeof(string));
                dt.Columns.Add("SubItem", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("SerialNumber", typeof(string));
                dt.Columns.Add("SubItemCode", typeof(string));
                dt.Columns.Add("Amt", typeof(string));
                DataTable dt2 = ds.Tables["Sheet1"];

                foreach (DataRow dr in dt2.Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
                dt.Rows[0].Delete();
                dt.AcceptChanges(); dt2.AcceptChanges();
                dt.Columns["PoNumber"].ColumnName = "PoNumber";
                dt.Columns["POCatagry"].ColumnName = "PoCatagry";
                dt.Columns["SendingTo"].ColumnName = "SendingTo";
                dt.Columns["Item"].ColumnName = "Item";
                dt.Columns["SubItem"].ColumnName = "SubItem";
                dt.Columns["Qty"].ColumnName = "Qty";
                dt.Columns["Amt"].ColumnName = "Amt";
                dt.Columns["SerialNumber"].ColumnName = "SerialNumber";
                dt.Columns["SubItemCode"].ColumnName = "SubItemCode";
                

                _purchaseOrder.blukUpload(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion
    }
}
