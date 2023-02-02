using System.Collections.Generic;

namespace WMSAPI.ViewModels
{
    public class StatesViewModel
    {
        public int state_id { get; set; }
        public string state_name { get; set; }
    }
    public class DistrictsViewModel
    {
        public int district_id { get; set; }
        public string district_name { get; set; }
    }
    public class Root
    {
        public List<DistrictsViewModel> districts { get; set; }
        public List<StatesViewModel> states { get; set; }
        public int ttl { get; set; }
    }
}
