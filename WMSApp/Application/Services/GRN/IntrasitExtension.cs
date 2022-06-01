using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
namespace Application.Services.GRN
{
    public static class IntrasitExtension
    {
        public static List<string> GetUniqePo(this List<IntrasitDb> source)
        {
            var query = from x in source
                        group x by x.PurchaseOrder into pos
                        select pos.Key;
            var result = query.ToList();
            return result;

        }
    }
}
