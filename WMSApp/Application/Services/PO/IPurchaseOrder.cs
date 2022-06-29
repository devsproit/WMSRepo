using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PO
{
    public interface IPurchaseOrder
    {
        void Insert(PurchaseOrderDb entiry);
        void Update(PurchaseOrderDb entiry);
    }
}
