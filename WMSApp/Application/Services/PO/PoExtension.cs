using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PO
{
    public static class PoExtension
    {
        public static List<string> GetUniqeCode(this List<SubItemDb> source)
        {
            var query = from x in source
                        group x by x.SubItemCode into pos
                        select pos.Key;
            var result = query.ToList();
            return result;

        }
    }
}
