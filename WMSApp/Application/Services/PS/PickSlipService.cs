using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.PS;

namespace Application.Services.PS
{
    public partial class PickSlipService : IPickSlipService
    {
        #region Fields
        private readonly IRepository<PickSlipMaster> _pickSlipMasterRepository;

        #endregion

        #region Ctor

        public PickSlipService(IRepository<PickSlipMaster> pickSlipMasterRepository)
        {
            _pickSlipMasterRepository = pickSlipMasterRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(PickSlipMaster entity)
        {
            _pickSlipMasterRepository.Insert(entity);
        }

        public virtual PickSlipMaster GetbyId(int id)
        {
            return _pickSlipMasterRepository.GetById(id);
        }

        public virtual IPagedList<PickSlipMaster> GetPickSlipMasters(string branchCode,string pickslipName = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _pickSlipMasterRepository.Table
                        select x;
            if (!string.IsNullOrEmpty(pickslipName))
                query = query.Where(x => x.PickSlipName.Contains(pickslipName));
            query = query.Where(x => x.BranchCode == branchCode);
            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<PickSlipMaster>(query, pageIndex, pageSize);
            return result;


        }
        #endregion
    }
}
