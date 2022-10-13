using Domain.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;

namespace Application.Services.Master
{
    public partial class DispatchService:IDispatchService
    {

        #region Fields
        private readonly IRepository<Dispatch> _dispatchRepository;
        #endregion
        #region Ctor
        public DispatchService(IRepository<Dispatch> dispatchRepository)
        {
            _dispatchRepository = dispatchRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(Dispatch entity)
        {
            _dispatchRepository.Insert(entity);
        }

        public virtual void Update(Dispatch entity)
        {
            _dispatchRepository.Update(entity);
        }

        public virtual Dispatch GetById(int id)
        {
            return _dispatchRepository.GetById(id);
        }

        public virtual IPagedList<Dispatch> GetAllDispatch(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _dispatchRepository.Table
                        select x;
            query = query.Where(x => x.BranchCode == branchCode);
            query = query.OrderByDescending(x => x.Id);
            var result = new PagedList<Dispatch>(query, pageIndex, pageSize);
            return result;
        }

        #endregion
    }
}
