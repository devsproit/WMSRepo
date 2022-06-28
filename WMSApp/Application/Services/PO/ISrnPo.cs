using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PO
{
    public interface ISrnPo
    {
        void Insert(SRNPoDb serviceOrderPO);
        void Update(SRNPoDb serviceOrderPO);
    }
}
