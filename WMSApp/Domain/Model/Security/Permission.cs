using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Security
{
    public class Permission
    {
        public int Id { get; set; }
        public int Permission_Id { get; set; }
        public String Role_ID { get; set; }

        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public string PermissionMaster { get; set; }
    }
}
