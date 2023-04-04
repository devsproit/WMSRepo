
using WMS.Core;

namespace Domain.Model.GRN
{
    public class Stocks : BaseEntity
    {
        public int Qty { get; set; }
        public string ItemCode { get; set; }

        public string SubItemName { get; set; }
        public string Remark { get; set; }
    }
}
