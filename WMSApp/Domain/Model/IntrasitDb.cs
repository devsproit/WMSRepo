using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model
{
    public class IntrasitDb:BaseEntity
    {
       
        public string Login_Branch { get; set; }
        public string Sender_Company { get; set; }
        public string Branch { get; set; }
        public string PurchaseOrder { get; set; }
        public string Item_Code { get; set; }
        public string SubItem_Code { get; set; }
        public string SubItem_Name { get; set; }
        public string MaterialDescription { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amt { get; set; }
        public DateTime ETA { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsGrn { get; set; }

    }
}
