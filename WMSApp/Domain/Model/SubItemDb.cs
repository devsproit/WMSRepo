using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SubItemDb
    {
        public int Id { get; set; }
        public string SubItemCode { get; set; }

       
        public string SubItemName { get; set; }
        public string ItemName { get; set; }

        public string MaterialDescription { get; set; }
       
        public string SubItemSize { get; set; }

 
        public string FOC { get; set; }


        public string SubItemCategory { get; set; }


        public string SubItemSR { get; set; }


    }
}
