using Domain.Model;
using System.Collections.Generic;

namespace WMSAPI.ViewModels.PO
{
    public class PurchaseOrderViewModel
    {
        public List<Branch> listBranch { get; set; }
        public List<SenderCompanyNameDb> listSenderCompany { get; set; }
        public List<ItemDb> listItem { get; set; }

        public List<SubItemDb> listSubItem { get; set; }
    }
}
