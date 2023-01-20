using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.GRN
{
    public partial class GoodReceivedNoteMaster : BaseEntity
    {
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; } = new DateTime();
        public string BranchCode { get; set; }
        public string SenderCompany { get; set; }
        public string GRNNumberOfSAP { get; set; }
        public string IRN { get; set; }

        private ICollection<GoodReceivedNoteDetails> _goodReceivedNoteDetails;

        public virtual ICollection<GoodReceivedNoteDetails> GoodReceivedNoteDetails
        {
            get => _goodReceivedNoteDetails ?? (_goodReceivedNoteDetails = new List<GoodReceivedNoteDetails>());
            protected set => _goodReceivedNoteDetails = value;
        }

    }
}
