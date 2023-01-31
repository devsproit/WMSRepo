using System;
using System.Collections.Generic;

namespace WMSAPI.ViewModels.Invoice
{
    public class InvoiceModel
    {
        public string InvoiceNo { get; set; }
        public List<InvoiceItemModel> items { get; set; }
        public string DocType { get; set; }
        public string PoNumber { get; set; }
    }
    public class InvoiceItemModel
    {
        public int Id { get; set; }
        public string PoNumber { get; set; }
       
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string Size { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }


    }
}
