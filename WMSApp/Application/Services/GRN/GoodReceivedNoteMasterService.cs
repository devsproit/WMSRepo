using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.GRN;
namespace Application.Services.GRN
{
    public partial class GoodReceivedNoteMasterService : IGoodReceivedNoteMasterService
    {
        #region Fields
        private readonly IRepository<GoodReceivedNoteMaster> _grnMasterRepository;

        #endregion

        #region Ctor
        public GoodReceivedNoteMasterService(IRepository<GoodReceivedNoteMaster> grnMasterRepository)
        {
            _grnMasterRepository = grnMasterRepository;
        }


        #endregion

        #region Methods
        public virtual IPagedList<GoodReceivedNoteMaster> GetAllMaster(string branch, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _grnMasterRepository.Table
                        select x;
            query=query.Where(x=>x.BranchCode==branch);
            var result = new PagedList<GoodReceivedNoteMaster>(query, pageIndex, pageSize);
            return result;
        }

        public virtual GoodReceivedNoteMaster GetbyId(int id)
        {
            return _grnMasterRepository.GetById(id);
        }

        public virtual void Insert(GoodReceivedNoteMaster entiry)
        {
            _grnMasterRepository.Insert(entiry);
        }

        public virtual void Update(GoodReceivedNoteMaster entiry)
        {
            _grnMasterRepository.Update(entiry);
        }
        #endregion
    }
}
