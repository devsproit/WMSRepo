using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.PS
{
    public partial class PickSlipMaster : BaseEntity
    {
        public string PickSlipName { get; set; }
        public string BranchCode { get; set; }
        public DateTime CreateOn { get; set; }
        public string DockType { get; set; }
        public int POID { get; set; }
        public bool IsProcessed { get; set; }
        private ICollection<PickSlipDetails> _pickSlipDetails;
        public virtual ICollection<PickSlipDetails> PickSlipDetails
        {
            get => _pickSlipDetails ?? (_pickSlipDetails = new List<PickSlipDetails>());
            protected set => _pickSlipDetails = value;
        }

    }
}
