using Domain.Model.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WMSWebApp.ViewModels.Security
{
    public class CreateRoleModel
    {
        [Required]
        public string Role { get; set; }
        public List<PermissionMaster> PermissionMaster { get; set; }
        [Required]
        public List<PermissionMap> PermissionMap { get; set; }

    }

    public class PermissionMap
    {
        public int PermissionId { get; set; }
        public string RoleId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public string PermissionName { get; set; }
    }
}
