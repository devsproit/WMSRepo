using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data
{
    static partial class MappingDefaults
    {
        public static string CompanyTable => "Company";
        public static string Itemtable => "Item";
        public static string SubItemtable => "SubItem";
        public static string BranchTableName => "Branch";
        public static string WarehouseTable => "Warehouse";

        public static string SalePoTable => "SalePo";

        public static string PurchaseOrderTable => "PurchaseOrder";

        public static string StockTransferTable => "StockTransferPo";

        public static string SRnTable => "SRNPo";

        public static string ServiceOrderTable => "ServiceOrderPO";

        public static string Stock => "ItemStock";


    }
}
