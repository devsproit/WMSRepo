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
    public class ServiceOrderPo : IServiceOrderPo
    {
        #region Fields
        private readonly IRepository<ServiceOrderPODb> _serviceOrderPoRepository;
        private readonly IRepository<PurchaseOrderDb> _poRepository;
        #endregion

        #region Ctor
        public ServiceOrderPo(IRepository<ServiceOrderPODb> ServiceOrderPoRepository, IRepository<PurchaseOrderDb> poRepository)
        {
            _serviceOrderPoRepository = ServiceOrderPoRepository;
            _poRepository = poRepository;
        }
        public void Insert(ServiceOrderPODb serviceOrderPO)
        {
            _serviceOrderPoRepository.Insert(serviceOrderPO);
        }

        public void Update(ServiceOrderPODb serviceOrderPO)
        {
            _serviceOrderPoRepository.Update(serviceOrderPO);
        }

        public virtual IPagedList<ServiceOrderPODb> GetServicePos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _serviceOrderPoRepository.Table
                        select x;
            if (poNumber != "0")
            {
                query = query.Where(x => x.PONumber == poNumber);
            }
            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<ServiceOrderPODb>(query, pageIndex, pageSize);
            return result;

        }

        public IPagedList<ServiceOrderPODb> GetDetails(string category, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _poRepository.Table
                        join y in _serviceOrderPoRepository.Table
                        on x.PONumber equals y.PONumber
                        where x.POCategory == category
                        select y;
            var result = new PagedList<ServiceOrderPODb>(query, pageIndex, pageSize);
            return result;
        }


        #endregion
    }
}
