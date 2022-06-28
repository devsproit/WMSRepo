using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.PO
{
    public interface IServiceOrderPo
    {
        void Insert(ServiceOrderPODb serviceOrderPO);
        void Update(ServiceOrderPODb serviceOrderPO);
    }
}
