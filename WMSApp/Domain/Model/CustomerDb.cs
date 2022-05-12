using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model
{
    public class CustomerDb:BaseEntity
    {
        
        public string ScreenCode { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Location { get; set; }

    }
}
