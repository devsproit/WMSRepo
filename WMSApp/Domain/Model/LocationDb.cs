using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class LocationDb
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }

    }
}
