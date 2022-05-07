using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CustomerDb
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Location { get; set; }

    }
}
