using Domain.Model.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WMSAPI.ViewModels.Security
{
    public class UpdateRolePermissionMaster
    {
        [Required]
        public string Role { get; set; }
        public List<PermissionMaster> PermissionMaster { get; set; }
        [Required]
        public List<PermissionMap> PermissionMap { get; set; }
        public List<PermissionMaster_Role_Mapping> RoleWisePermission { get; set; }
        public string RoleId { get; set; }
    }
}
