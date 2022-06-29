﻿using Application.Services;
using Application.Services.PO;
using AutoMapper;
using Domain.Model;
using Domain.Model.PO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        #endregion

        #region Ctor
        public PoController(IMapper mapper, IPurchaseOrder purchaseOrder, ISubItemHelper SubItemHelper, IBranchHelper BranchHelper, IItemHelper ItemHelper, ISenderCompany senderCompany,
            ISalePo salePo, IStockTransferPo stockTransferPo, ISrnPo srnPo,ICustomerHelper customerHelper,IServiceOrderPo serviceOrderPo)
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

         }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
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
            listSubItem.Insert(0, new SubItemDb { Id = 0, SubItemName = "Select" });
            ViewBag.ListofSenderCompany = listSendCompany;
            ViewBag.listBranch = listBranch;
            ViewBag.listItem = listItem;
            ViewBag.listSubItem = listSubItem;

            purchaseOrderViewModel.listItem=listItem;
            purchaseOrderViewModel.listSubItem=listSubItem;
            purchaseOrderViewModel.listBranch=listBranch;
            purchaseOrderViewModel.listSenderCompany = listSendCompany;
            return View();
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] PoViewModel poViewModel)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                PurchaseOrderDb purchaseOrderDb = new PurchaseOrderDb();
                purchaseOrderDb.POCategory = poViewModel.POCatrgory;
                purchaseOrderDb.PODate = currentDate;
                purchaseOrderDb.PONumber = poViewModel.PONumber;
                if (poViewModel.stockTransferCategories != null)
                {
                    _purchaseOrder.Insert(purchaseOrderDb);
                    foreach (var item in poViewModel.stockTransferCategories)
                    {
                        StockTransferPoDb stockTransferPo = new StockTransferPoDb();
                        stockTransferPo.StockTransferPOCategory = item.StockTransferPOCategory;
                        stockTransferPo.StockTransferPOSendingTo = item.StockTransferPOSendingTo;
                        stockTransferPo.StockTransferPOItem = item.StockTransferPOItem;
                        stockTransferPo.StockTransferPOSubItem = item.StockTransferPOSubItem;
                        stockTransferPo.StockTransferPOQty = item.StockTransferPOQty;
                        stockTransferPo.StockTransferPOAmt = item.StockTransferPOAmt;
                        stockTransferPo.StockTransferPOSerialNumber = item.StockTransferPOSerialNumber;
                        stockTransferPo.PONumber = poViewModel.PONumber;
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
                        serviceOrderPODb.ServiceOrderPOSubitem = item.ServiceOrderPOSubitem;
                        serviceOrderPODb.ServiceOrderPOQty = item.ServiceOrderPOQty;
                        serviceOrderPODb.ServiceOrderPOAmt = item.ServiceOrderPOAmt;
                        serviceOrderPODb.ServiceOrderPOSerialNumber = item.ServiceOrderPOSerialNumber;
                        serviceOrderPODb.PONumber = poViewModel.PONumber;
                        serviceOrderPODb.ServiceOrderPOServiceCatagry = item.ServiceOrderPOServiceCatagry;
                        serviceOrderPODb.ServiceOrderPOServiceRequestNumber = item.ServiceOrderPOServiceRequestNumber;
                        serviceOrderPODb.ServiceOrderPOSalePO = item.ServiceOrderPOSalePO;
                        serviceOrderPODb.ServiceOrderPOSaleDate = item.ServiceOrderPOSaleDate;
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
                        salePoDb.SalePOSubItem = item.SalePOSubItem;
                        salePoDb.SalePOQty = item.SalePOQty;
                        salePoDb.SalePOAmt = item.SalePOAmt;
                        salePoDb.SalePOSerialNumber = item.SalePOSerialNumber;
                        salePoDb.PONumber = poViewModel.PONumber;
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
                        sRNPo.SrnPOSubItem = item.SrnPOSubItem;
                        sRNPo.PONumber = poViewModel.PONumber;
                        _srnPo.Insert(sRNPo);
                    }
                    _purchaseOrder.Insert(purchaseOrderDb);
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
        public JsonResult GetAmtTo(string subName,string type)
        {
            var data = _SubItemHelper.GetSubItemCustomerAmt(subName,type);
            return Json(data);
        }
        #endregion

    }
}
