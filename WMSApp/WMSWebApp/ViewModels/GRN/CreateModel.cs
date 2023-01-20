using System.Collections.Generic;
using Domain.Model.Masters;
namespace WMSWebApp.ViewModels.GRN
{
    public class CreateModel
    {

        public int ItemId { get; set; }
        public int zone { get; set; }
        public int WarehouseId { get; set; }
        public int Warea { get; set; }
        public string Remark { get; set; }
        public string wcode { get; set; }
        public string zcode { get; set; }
        public string acode { get; set; }
        public string Ponumber { get; set; }
        public string invoice { get; set; }
        public string GRNNumberOfSAP { get; set; }
        public string IRN { get; set; }

        public bool AllowGRN { get; set; }


        public List<VendorVehicle> Vehicles { get; set; }

    }
}
