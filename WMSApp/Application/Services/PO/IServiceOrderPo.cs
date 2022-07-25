using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.PO
{
    public interface IServiceOrderPo
    {
        void Insert(ServiceOrderPODb serviceOrderPO);
        void Update(ServiceOrderPODb serviceOrderPO);
        IPagedList<ServiceOrderPODb> GetServicePos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
