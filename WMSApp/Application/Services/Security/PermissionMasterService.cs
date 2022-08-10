
using Domain.Model.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WMS.Core;
using WMS.Core.Data;
using WMS.Data;
namespace Application.Services.Security
{
    public class PermissionMasterService : IPermissionMasterService
    {
        #region Fields
        private readonly IRepository<PermissionMaster> _permissionMasterRepository;
        private readonly IWorkContext _workContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<PermissionMaster_Role_Mapping> _permissionMasterRoleMappingRepository;

        #endregion

        #region Ctor
        public PermissionMasterService(IRepository<PermissionMaster> permissionMasterRepository,
            IWorkContext workContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,

            IRepository<PermissionMaster_Role_Mapping> permissionMasterRoleMappingRepository)
        {
            _permissionMasterRepository = permissionMasterRepository;
            _workContext = workContext;
            _roleManager = roleManager;
            _userManager = userManager;

            _permissionMasterRoleMappingRepository = permissionMasterRoleMappingRepository;
        }

        #endregion


        #region Methods
        public virtual void AddPermissionMaster(PermissionMaster entity)
        {
            _permissionMasterRepository.Insert(entity);
        }

        public virtual PermissionMaster GetPermissionById(int id)
        {
            return _permissionMasterRepository.GetById(id);
        }
        public virtual void UpdatePermissionMaster(PermissionMaster entity)
        {
            _permissionMasterRepository.Update(entity);
        }

        public virtual IPagedList<PermissionMaster> GetAllPermissionMaster(string permission = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _permissionMasterRepository.Table;
            if (!string.IsNullOrEmpty(permission))
                query = query.Where(x => x.Permission.Contains(permission));
            query = query.OrderByDescending(x => x.Id);
            return new PagedList<PermissionMaster>(query, pageIndex, pageSize);
        }


        protected virtual IList<Permission> GetPermissionRecordsByCustomerRoleId(string customerRoleId)
        {

            var permission = _permissionMasterRoleMappingRepository.Table
                .Where(x => x.Role_ID == customerRoleId)
                .Select(x =>
               new Permission
               {

                   Add = x.Add,
                   Delete = x.Delete,
                   Edit = x.Edit,
                   Id = x.Id,
                   Permission_Id = x.Permission_Id,
                   Role_ID = x.Role_ID,
                   View = x.View,
                   PermissionMaster = x.PermissionMaster.Permission


               });
            //var getList = _mongoRepository.GetOne(x => x.Role_ID == customerRoleId);
            return permission.ToList();
        }


        public virtual async Task<bool> Authorize(PermissionMaster permission, PermissionType permissionType)
        {
            var user = await _workContext.GetCurrentUserAsync();
            return await Authorize(permission.Permission, user, permissionType);
        }

        public virtual async Task<bool> Authorize(string permission, ApplicationUser applicationUser, PermissionType permissionType)
        {
            if (string.IsNullOrEmpty(permission))
                return false;
            var customerRoles = await _userManager.GetRolesAsync(applicationUser);

            foreach (var role in customerRoles)
            {
                var roleId = await _roleManager.FindByNameAsync(role);
                if (Authorize(permission, roleId.Id, permissionType))
                    return true;
            }
            //_roleManager.GetRoleIdAsync(new IdentityRole(identityUser.))
            //no permission found
            return false;
        }

        public virtual bool Authorize(string permission, string roleId, PermissionType permissionType)
        {
            if (string.IsNullOrEmpty(permission))
                return false;
            var permissionList = GetPermissionRecordsByCustomerRoleId(roleId);
            foreach (var item in permissionList)
            {
                if (item.PermissionMaster.Equals(permission, StringComparison.InvariantCultureIgnoreCase))
                {
                    bool p = false;
                    switch (permissionType)
                    {
                        case PermissionType.Add:
                            if (item.Add)
                                p = true;
                            break;
                        case PermissionType.Edit:
                            if (item.Edit)
                                p = true;
                            break;
                        case PermissionType.View:
                            if (item.View)
                                p = true;
                            break;
                        case PermissionType.Delete:
                            if (item.Delete)
                                p = true;
                            break;
                        default:
                            p = false;
                            break;
                    }
                    return p;

                }

            }
            return false;
        }
        #endregion
    }
}
