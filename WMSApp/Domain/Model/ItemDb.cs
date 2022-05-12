using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model
{
    public class ItemDb:BaseEntity
    {
        
        public string CompanyName { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        
    }
}
