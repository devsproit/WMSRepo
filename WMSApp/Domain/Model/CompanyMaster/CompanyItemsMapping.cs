using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.CompanyMaster
{
    public partial class CompanyItemsMapping:BaseEntity
    {
        public int CompanyId { get; set; }
        public int ItemId { get; set; }
        public virtual CompanyDb Company { get; set; } 

        public virtual ItemDb Item { get; set; }

    }
}
