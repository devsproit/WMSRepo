using WMS.Core;

namespace WMS.Data.Mapping.BranchMaster
{
    public partial class UserBranchMapping:BaseEntity
    {
        public int BranchId { get; set; }
        public string RefGuid { get; set; }

    }
}
