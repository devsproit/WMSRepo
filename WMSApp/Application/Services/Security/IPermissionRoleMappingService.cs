using Domain.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public partial interface IPermissionRoleMappingService
    {
        void Insert(List<PermissionMaster_Role_Mapping> entity);
        void Update(List<PermissionMaster_Role_Mapping> entity);
        List<PermissionMaster_Role_Mapping> GetPermissionMapListRoleWise(string roleId);
    }
}
