using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using Domain.Model.Invoice;
using WMS.Core;

namespace Application.Services.Invoice
{
    public partial interface IInvoiceService
    {
        int InsertMaster(InvoiceMaster entity);
        void InsertDetails(InvoiceDetails entity);
        void InsertDetails(List<InvoiceDetails> entities);
        IPagedList<InvoiceMaster> GetAllMaster(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue, string searchType = "ALL");
        InvoiceMaster GetById(int id);
        void Update(InvoiceMaster entity);
    }
}
