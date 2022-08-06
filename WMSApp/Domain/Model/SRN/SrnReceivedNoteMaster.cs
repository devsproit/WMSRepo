using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.SRN
{
    public partial class SrnReceivedNoteMaster : BaseEntity
    {
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; } = new DateTime();
        public string BranchCode { get; set; }
        public string SenderCompany { get; set; }

        private ICollection<SrnNoteDetails> _srnReceivedNoteDetails;

        public virtual ICollection<SrnNoteDetails> SrnNoteDetails
        {
            get => _srnReceivedNoteDetails ?? (_srnReceivedNoteDetails = new List<SrnNoteDetails>());
            protected set => _srnReceivedNoteDetails = value;
        }

    }
}
