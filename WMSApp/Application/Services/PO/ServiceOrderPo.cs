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

        #endregion

        #region Ctor
        public ServiceOrderPo(IRepository<ServiceOrderPODb> ServiceOrderPoRepository)
        {
            _serviceOrderPoRepository = ServiceOrderPoRepository;
        }
        public void Insert(ServiceOrderPODb serviceOrderPO)
        {
            _serviceOrderPoRepository.Insert(serviceOrderPO);
        }

        public void Update(ServiceOrderPODb serviceOrderPO)
        {
            _serviceOrderPoRepository.Update(serviceOrderPO);
        }

       
        #endregion
    }
}
