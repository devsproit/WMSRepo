using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Model.PO;

namespace Application.Services.SRN
{
    public static class SrnExtension
    {
        public static List<string> GetUniqePoOrder(this List<PurchaseOrderDb> source)
        {
            var query = from x in source
                        group x by x.PONumber into pos
                        select pos.Key;
            var result = query.ToList();
            return result;

        }
    }
}
