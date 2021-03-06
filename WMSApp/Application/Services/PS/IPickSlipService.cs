using Domain.Model.PS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.PS
{
    public partial interface IPickSlipService
    {
        void Insert(PickSlipMaster entity);
        PickSlipMaster GetbyId(int id);

        IPagedList<PickSlipMaster> GetPickSlipMasters(string pickslipName = "", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
