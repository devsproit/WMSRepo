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
        ServiceOrderPODb GetById(int id);
        IPagedList<ServiceOrderPODb> GetServicePos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<ServiceOrderPODb> GetDetails(string category, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<ServiceOrderPODb> GetAllServiceOrderPo(string status, string branchCode, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
