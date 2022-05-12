using WMS.Core;
namespace Domain.Model
{


    public class CompanyDb : BaseEntity
    {

        public string ScreenCode { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPersonHO { get; set; }
        public string ContactNumberHO { get; set; }
        public string EmailIdHO { get; set; }
        public string SpaceSizeFormat { get; set; }
        public int Items { get; set; }
        public int SubItem { get; set; }
    }
}