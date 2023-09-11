﻿using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Application.Services.PO
{
    public interface IPurchaseOrder
    {
        void Insert(PurchaseOrderDb entiry);
        void Update(PurchaseOrderDb entiry);

        IPagedList<PurchaseOrderDb> GetPurchaseOrders(string branchCode, string category, bool status = false, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<PurchaseOrderDb> GetPendingPO(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue);

        PurchaseOrderDb GetById(int id);

        void blukUpload(DataTable ds);

        List<PurchaseOrderDb> GetAllPurchaseByPoNumber(string poNumber);
    }
}