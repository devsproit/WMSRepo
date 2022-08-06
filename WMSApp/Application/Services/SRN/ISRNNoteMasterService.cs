using Domain.Model.SRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SRN
{
    public partial interface ISRNNoteMasterService
    {
        void Insert(SrnReceivedNoteMaster entiry);
        void Update(SrnReceivedNoteMaster entiry);
        //GoodReceivedNoteMaster GetbyId(int id);
        //IPagedList<GoodReceivedNoteMaster> GetAllMaster(string branch, int pageIndex = 0, int pageSize = int.MaxValue);

        //IPagedList<Stocks> GetStocks(string branchcode, string itemcode = null, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
