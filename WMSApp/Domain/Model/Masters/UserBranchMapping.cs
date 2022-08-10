using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Masters
{
    public partial class UserBranchMapping:BaseEntity
    {
        public int BranchId { get; set; }
        public string RefGuid { get; set; }
    }
}
