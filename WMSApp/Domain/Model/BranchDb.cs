﻿using WMS.Core;
namespace Domain.Model
{

    public class BranchDb : BaseEntity
    {

        public string ScreenCode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int CompanyId { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPersonBranch { get; set; }
        public string ContactNumberBranch { get; set; }
        public string EmailIdBranch { get; set; }
        public string AssociatedEmployee { get; set; }
        public int WarehouseId { get; set; }

    }
}