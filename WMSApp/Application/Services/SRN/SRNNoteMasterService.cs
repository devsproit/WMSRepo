using Domain.Model.SRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

namespace Application.Services.SRN
{
   
    public partial class SRNNoteMasterService : ISRNNoteMasterService
    {
        #region Fields
        private readonly IRepository<SrnReceivedNoteMaster> _srnMasterRepository;
        private readonly IRepository<SrnReceivedNoteDetails> _srnDetailsRepository;

        #endregion

        #region Ctor
        public SRNNoteMasterService(IRepository<SrnReceivedNoteMaster> srnMasterRepository, IRepository<SrnReceivedNoteDetails> srnDetailsRepository)
        {
            _srnMasterRepository = srnMasterRepository;
            _srnDetailsRepository = srnDetailsRepository;
        }

        public void Insert(SrnReceivedNoteMaster entiry)
        {
            _srnMasterRepository.Insert(entiry);
        }

        public void Update(SrnReceivedNoteMaster entiry)
        {
            _srnMasterRepository.Update(entiry);
        }




        #endregion

        #region Methods

        #endregion
    }
}
