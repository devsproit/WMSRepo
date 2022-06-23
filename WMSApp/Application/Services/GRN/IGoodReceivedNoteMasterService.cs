using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.GRN;
using WMS.Core;

namespace Application.Services.GRN
{
    public partial interface IGoodReceivedNoteMasterService
    {
        void Insert(GoodReceivedNoteMaster entiry);
        void Update(GoodReceivedNoteMaster entiry);
        GoodReceivedNoteMaster GetbyId(int id);
        IPagedList<GoodReceivedNoteMaster> GetAllMaster(string branch, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
