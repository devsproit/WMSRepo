using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.GRN
{
    public partial class GoodReceivedNoteDetails:BaseEntity
    {
        public int GRNId { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
        public virtual GoodReceivedNoteMaster GoodReceivedNoteMaster { get; set; }

    }
}
