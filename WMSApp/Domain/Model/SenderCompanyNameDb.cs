using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SenderCompanyNameDb
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public string SenderCompanyCode { get; set; }
        public string SenderCompanyName { get; set; }
        
    }
}
