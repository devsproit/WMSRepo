using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Security
{
    public partial class PermissionMaster_Role_Mapping : BaseEntity
    {
        public int Permission_Id { get; set; }
        public String Role_ID { get; set; }

        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }

        public virtual PermissionMaster PermissionMaster { get; set; }
    }
}
