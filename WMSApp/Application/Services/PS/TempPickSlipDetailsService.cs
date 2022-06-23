using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using Domain.Model.PS;
using WMS.Core;
namespace Application.Services.PS
{
    public partial class TempPickSlipDetailsService : ITempPickSlipDetailsService
    {
        #region Fields
        private readonly IRepository<TempPickSlipDetails> _tempPickSlipDetailsRepository;

        #endregion

        #region Ctor
        public TempPickSlipDetailsService(IRepository<TempPickSlipDetails> tempPickSlipDetailsRepository)
        {
            _tempPickSlipDetailsRepository = tempPickSlipDetailsRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(TempPickSlipDetails entity)
        {
            _tempPickSlipDetailsRepository.Insert(entity);
        }

        public virtual void Update(TempPickSlipDetails entity)
        {
            _tempPickSlipDetailsRepository.Update(entity);
        }

        public virtual TempPickSlipDetails GetById(int id)
        {
            return _tempPickSlipDetailsRepository.GetById(id);
        }

        public virtual IPagedList<TempPickSlipDetails> GetAllTemp(string guid, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _tempPickSlipDetailsRepository.Table
                        select x;
            query = query.Where(x => x.Guid == guid);
            var result = new PagedList<TempPickSlipDetails>(query, pageIndex, pageSize);
            return result;
        }
        #endregion
    }

}
