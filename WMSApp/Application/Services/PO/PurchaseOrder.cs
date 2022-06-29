using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

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
        public void Insert(PurchaseOrderDb entiry)
        {
            _poRepository.Insert(entiry);
        }

        public void Update(PurchaseOrderDb entiry)
        {
            _poRepository.Update(entiry);
        }


    }
}
