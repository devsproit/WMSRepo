using Domain.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public class StandardPermissionProvider : IPermissionProvider
    {
        public static readonly PermissionMaster User = new PermissionMaster { Permission = "User" };
        public static readonly PermissionMaster Branch = new PermissionMaster { Permission = "Branch" };
        public static readonly PermissionMaster Company = new PermissionMaster { Permission = "Company" };
        public static readonly PermissionMaster Customer = new PermissionMaster { Permission = "Customer" };
        public static readonly PermissionMaster ItemMaster = new PermissionMaster { Permission = "ItemMaster" };
        public static readonly PermissionMaster SubItemMaster = new PermissionMaster { Permission = "SubItemMaster" };
        public static readonly PermissionMaster Warehouse = new PermissionMaster { Permission = "Warehouse" };
        public static readonly PermissionMaster Intrasit = new PermissionMaster { Permission = "Intrasit" };
        public static readonly PermissionMaster GRN = new PermissionMaster { Permission = "GRN" };
        public static readonly PermissionMaster PurchaseOrder = new PermissionMaster { Permission = "PurchaseOrder" };
        public static readonly PermissionMaster PickSlip = new PermissionMaster { Permission = "PickSlip" };
        public static readonly PermissionMaster Invoice = new PermissionMaster { Permission = "Invoice" };
        public static readonly PermissionMaster Dashboard = new PermissionMaster { Permission = "Dashboard" };
        public static readonly PermissionMaster Permission = new PermissionMaster { Permission = "Permission" };
       

        public virtual IEnumerable<PermissionMaster> GetPermissions()
        {
            return new[]
            {
               
                 Dashboard,
                 Customer,
                 ItemMaster,
                 SubItemMaster,
                 Warehouse,
                 Intrasit,
                 PurchaseOrder,
                 PickSlip,
                 User,
                 Branch,
                 Company,
                 GRN,
                 Invoice,
                 Permission
            };
        }
    }
}
