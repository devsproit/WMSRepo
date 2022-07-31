using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Security
{
    public class PermissionMaster : BaseEntity
    {
        private ICollection<PermissionMaster_Role_Mapping> _permissionMaster_Role_Mappings;
        public String Permission { get; set; }
        public DateTime CreateOn { get; set; }
        public string SystemName { get; set; }

        public virtual ICollection<PermissionMaster_Role_Mapping> PermissionMaster_Role_Mappings
        {
            get => _permissionMaster_Role_Mappings ?? (_permissionMaster_Role_Mappings = new List<PermissionMaster_Role_Mapping>());
            protected set => _permissionMaster_Role_Mappings = value;

        }
    }
}
