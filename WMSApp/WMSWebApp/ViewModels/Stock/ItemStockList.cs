using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;

namespace WMSWebApp.ViewModels.Stock
{
    public class ItemStockList
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Remark { get; set; }
        public string Location { get; set; }
        public string LastUpdateDate { get; set; }
    }
}
