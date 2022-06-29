using Domain.Model.PO;
using System;
using System.Collections.Generic;

namespace WMSWebApp.ViewModels.PO
{
    public class PoViewModel
    {
        public DateTime PODate { get; set; }

        public string PONumber { get; set; }

        public string POCatrgory { get; set; }

        public List<StockTransferCategory> stockTransferCategories { get; set; }

        public List<SalePO> salePOCategories { get; set; }

        public List<SRNPOCategory> sRNPOCategories { get; set; }


        public List<ServiceOrderCategory> serviceOrderCategories { get; set; }


    }
}
