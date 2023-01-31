using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Security;
using Domain.Model.Security;
using WMS.Web.Framework.Infrastructure.Extentsion;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WMSAPI.ViewModels.Security;

namespace WMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PermissionMasterController : BaseAdminController
    {
        #region Fields
        private readonly IPermissionMasterService _permissionMasterServcie;
        private readonly IPermissionRoleMappingService _permissionRoleMappingService;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Ctor
        public PermissionMasterController(IPermissionMasterService permissionMasterServcie, IPermissionRoleMappingService permissionRoleMappingService, RoleManager<IdentityRole> roleManager)
        {
            _permissionMasterServcie = permissionMasterServcie;
            _permissionRoleMappingService = permissionRoleMappingService;
            _roleManager = roleManager;
        }
        #endregion

        #region Methods
        [NonAction]
        public IActionResult Index()
        {
            return RedirectToAction("RoleList");
        }

        [NonAction]
        public IActionResult List()
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.Permission, PermissionType.View).Result)
            {
                return AccessDeniedView();
            }
            return View();
        }











        [NonAction]
        public IActionResult RoleList()
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.Permission, PermissionType.View).Result)
            {
                return AccessDeniedView();
            }
            return View();
        }


        [NonAction]
        public IActionResult CreateRole()
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.Permission, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            var Permission = _permissionMasterServcie.GetAllPermissionMaster().ToList();
            CreateRoleModel model = new CreateRoleModel();
            model.PermissionMaster = Permission;
            return View(model);
        }

        [NonAction]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                bool roleStatus = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleStatus)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.Role));

                    if (roleResult.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(model.Role);
                        List<PermissionMaster_Role_Mapping> permissionMaster_Role_Mappings = new List<PermissionMaster_Role_Mapping>();
                        foreach (var item in model.PermissionMap)
                        {
                            PermissionMaster_Role_Mapping permissionMaster_Role_Mapping = new PermissionMaster_Role_Mapping();
                            permissionMaster_Role_Mapping.Permission_Id = item.PermissionId;
                            permissionMaster_Role_Mapping.Role_ID = role.Id;
                            permissionMaster_Role_Mapping.View = item.View;
                            permissionMaster_Role_Mapping.Edit = item.Edit;
                            permissionMaster_Role_Mapping.Add = item.Add;
                            permissionMaster_Role_Mapping.Delete = item.Delete;
                            permissionMaster_Role_Mappings.Add(permissionMaster_Role_Mapping);

                        }
                        _permissionRoleMappingService.Insert(permissionMaster_Role_Mappings);
                        SuccessNotification("Successfully Role Created.");
                        return RedirectToAction("RoleList");
                    }
                    else
                    {
                        var Permission = _permissionMasterServcie.GetAllPermissionMaster().ToList();
                        model.PermissionMaster = Permission;
                        ModelState.AddModelError("RoleError", "Error in save new role");
                        return View(model);
                    }
                }
                else
                {
                    var Permission = _permissionMasterServcie.GetAllPermissionMaster().ToList();
                    model.PermissionMaster = Permission;
                    ModelState.AddModelError("RoleExist", "Error already exist.");
                    return View(model);
                }
            }
            else
            {
                var Permission = _permissionMasterServcie.GetAllPermissionMaster().ToList();
                model.PermissionMaster = Permission;
                return View(model);
            }

        }

        [NonAction]
        public async Task<IActionResult> UpdateRole(string id)
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.Permission, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var roleWisePermission = _permissionRoleMappingService.GetPermissionMapListRoleWise(id);
            UpdateRolePermissionMaster model = new UpdateRolePermissionMaster();
            model.RoleWisePermission = roleWisePermission;
            model.Role = role.Name;
            model.RoleId = role.Id;
            List<PermissionMap> pm = new List<PermissionMap>();
            foreach (var item in roleWisePermission)
            {
                PermissionMap m = new PermissionMap();
                m.Add = item.Add;
                m.Delete = item.Delete;
                m.Edit = item.Edit;
                m.PermissionId = item.Permission_Id;
                m.View = item.View;
                m.PermissionName = item.PermissionMaster.Permission;
                m.RoleId = item.Role_ID;
                pm.Add(m);
            }
            model.PermissionMap = pm;
            return View(model);
        }
        [NonAction]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRolePermissionMaster model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Role);
                List<PermissionMaster_Role_Mapping> permissionMaster_Role_Mappings = new List<PermissionMaster_Role_Mapping>();
                foreach (var item in model.PermissionMap)
                {
                    PermissionMaster_Role_Mapping permissionMaster_Role_Mapping = new PermissionMaster_Role_Mapping();
                    permissionMaster_Role_Mapping.Permission_Id = item.PermissionId;
                    permissionMaster_Role_Mapping.Role_ID = role.Id;
                    permissionMaster_Role_Mapping.View = item.View;
                    permissionMaster_Role_Mapping.Edit = item.Edit;
                    permissionMaster_Role_Mapping.Add = item.Add;
                    permissionMaster_Role_Mapping.Delete = item.Delete;
                    permissionMaster_Role_Mappings.Add(permissionMaster_Role_Mapping);

                }
                _permissionRoleMappingService.Update(permissionMaster_Role_Mappings);
                SuccessNotification("Successfully Role Updated.");

                return RedirectToAction("RoleList");
            }
            else
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var roleWisePermission = _permissionRoleMappingService.GetPermissionMapListRoleWise(model.RoleId);
                model.RoleWisePermission = roleWisePermission;
                model.Role = role.Name;
                model.RoleId = role.Id;
                return View(model);
            }

        }
        #endregion
    }
}
