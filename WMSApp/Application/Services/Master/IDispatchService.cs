using Domain.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.Master
{
    public partial interface IDispatchService
    {
        void Insert(Dispatch entity);
        void Update(Dispatch entity);
        Dispatch GetById(int id);
        IPagedList<Dispatch> GetAllDispatch(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
