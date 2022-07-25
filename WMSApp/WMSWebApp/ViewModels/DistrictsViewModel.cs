using System.Collections.Generic;

namespace WMSWebApp.ViewModels
{
    public class DistrictsViewModel
    {
        public int district_id { get; set; }
        public string district_name { get; set; }
    }
    public class Root
    {
        public List<DistrictsViewModel> districts { get; set; }
        public int ttl { get; set; }
    }
}
