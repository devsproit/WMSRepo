using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using Domain.Model.CompanyMaster;
namespace Domain.Model
{
    public class ItemDb : BaseEntity
    {

        public string CompanyName { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }

        private ICollection<CompanyItemsMapping> _companyItemsMappings;
        public virtual ICollection<CompanyItemsMapping> CompanyItemsMappings
        {
            get => _companyItemsMappings ?? (_companyItemsMappings = new List<CompanyItemsMapping>());
            protected set => _companyItemsMappings = value;
        }




    }
}
