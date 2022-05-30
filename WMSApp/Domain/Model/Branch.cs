using Domain.Model.Masters;
using System.Collections.Generic;
using WMS.Core;
namespace Domain.Model
{

    public class Branch : BaseEntity
    {

       
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int CompanyId { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPersonBranch { get; set; }
        public string ContactNumberBranch { get; set; }
        public string EmailIdBranch { get; set; }
        public string AssociatedEmployee { get; set; }
       

        private ICollection<BranchWiseWarehouse> _branchWiseWarehouses;
        public virtual ICollection<BranchWiseWarehouse> BranchWiseWarehouses
        {
            get => _branchWiseWarehouses ??= new List<BranchWiseWarehouse>();
            protected set => _branchWiseWarehouses = value;
        }

    }
}