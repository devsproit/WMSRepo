using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.PO
{
    public interface ISrnPo
    {
        void Insert(SRNPoDb serviceOrderPO);
        void Update(SRNPoDb serviceOrderPO);

       // public SRNPoDb GetAll(string PoNo);

        IPagedList<SRNPoDb> GetSrnDetails(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue);

        SRNPoDb GetById(int id);
    }
}
