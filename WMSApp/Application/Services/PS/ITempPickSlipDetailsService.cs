using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using Domain.Model.PS;
namespace Application.Services.PS
{
    public interface ITempPickSlipDetailsService
    {
        void Insert(TempPickSlipDetails entity);
        void Update(TempPickSlipDetails entity);
        TempPickSlipDetails GetById(int id);
        IPagedList<TempPickSlipDetails> GetAllTemp(string guid, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
