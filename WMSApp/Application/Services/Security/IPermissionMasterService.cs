using WMS.Core;
using Domain.Model.Security;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Data;

namespace Application.Services.Security
{
    public partial interface IPermissionMasterService
    {
        void AddPermissionMaster(PermissionMaster entity);
        void UpdatePermissionMaster(PermissionMaster entity);

        IPagedList<PermissionMaster> GetAllPermissionMaster(string permission = "", int pageIndex = 0, int pageSize = int.MaxValue);



        PermissionMaster GetPermissionById(int id);





        Task<bool> Authorize(PermissionMaster permission, PermissionType permissionType);

        Task<bool> Authorize(string permission, ApplicationUser applicationUser, PermissionType permissionType);
        bool Authorize(string permission, string roleId, PermissionType permissionType);
    }
}
