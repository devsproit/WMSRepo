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
        private readonly IRepository<PickSlipDetails> _pickSlipDetailsRepository;

        #endregion

        #region Ctor

        public PickSlipService(IRepository<PickSlipMaster> pickSlipMasterRepository, IRepository<PickSlipDetails> pickSlipDetailsRepository)
        {
            _pickSlipMasterRepository = pickSlipMasterRepository;
            _pickSlipDetailsRepository = pickSlipDetailsRepository;
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

        public virtual void Update(PickSlipMaster entity)
        {
            _pickSlipMasterRepository.Update(entity);
        }
        public virtual IPagedList<PickSlipMaster> GetPickSlipMasters(string branchCode, string pickslipName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool hideProcessed = false)
        {
            var query = from x in _pickSlipMasterRepository.Table
                        select x;
            if (!string.IsNullOrEmpty(pickslipName))
                query = query.Where(x => x.Id.ToString().Contains(pickslipName));
            query = query.Where(x => x.BranchCode == branchCode);

            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<PickSlipMaster>(query, pageIndex, pageSize);
            return result;


        }
        public virtual IPagedList<PickSlipDetails> GetAllPickSlip(string branchCode, string status = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _pickSlipDetailsRepository.Table
                        select x;
            
            query = query.Where(x => x.PickSlipMaster.BranchCode == branchCode);
            if (status=="ALL")
            {
                
            }
            else if (status=="Done")
            {
                query = query.Where(x => x.PickSlipMaster.IsProcessed == true);
            }
            else
            {
                query = query.Where(x => x.PickSlipMaster.IsProcessed == false);
            }
            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<PickSlipDetails>(query, pageIndex, pageSize);
            return result;


        }
        #endregion
    }
}
