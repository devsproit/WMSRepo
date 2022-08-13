using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using WMS.Core;
namespace Application.Services.PO
{
    public partial class PurchaseOrder : IPurchaseOrder
    {
        #region Fields
        private readonly IRepository<PurchaseOrderDb> _poRepository;

        #endregion

        #region Ctor
        public PurchaseOrder(IRepository<PurchaseOrderDb> poRepository)
        {
            _poRepository = poRepository;
        }


        #endregion

        #region Methods

        
        public void Insert(PurchaseOrderDb entiry)
        {
            _poRepository.Insert(entiry);
        }

        public void Update(PurchaseOrderDb entiry)
        {
            _poRepository.Update(entiry);
        }

        public virtual IPagedList<PurchaseOrderDb> GetPurchaseOrders(string branchCode, string category, bool status = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _poRepository.Table
                        where x.BranchCode == branchCode && x.POCategory == category
                        select x;
            query = query.Where(x => x.ProcessStatus == status);
            query = query.OrderBy(x => x.Id);
            var result = new PagedList<PurchaseOrderDb>(query, pageIndex, pageSize);
            return result;

        }

        public virtual PurchaseOrderDb GetById(int id)
        {
            return _poRepository.GetById(id);
        }

        #endregion
    }
}
