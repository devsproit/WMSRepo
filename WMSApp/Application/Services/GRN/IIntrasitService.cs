using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.GRN
{
    public interface IIntrasitService
    {
        IPagedList<IntrasitDb> GetPendingPO(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue);
        IntrasitDb GetById(int id);
    }
}
