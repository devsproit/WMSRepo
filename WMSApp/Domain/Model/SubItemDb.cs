using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WMS.Core;

namespace Domain.Model
{
    public class SubItemDb:BaseEntity
    {
        
        public string SubItemCode { get; set; }

       
        public string SubItemName { get; set; }
        public int ItemId { get; set; }

        public string MaterialDescription { get; set; }
       
        public string SubItemSize { get; set; }

 
        public string FOC { get; set; }


        public string SubItemCategory { get; set; }


        public string SubItemSR { get; set; }


    }
}
