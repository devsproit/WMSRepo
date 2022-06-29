using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model
{
    public class SenderCompanyNameDb :BaseEntity
    {
        
        public string CompanyCode { get; set; }
        public string Sender_Company_Code { get; set; }
        public string Sender_Company_Name { get; set; }
        
    }
}
