using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Customer
    {

        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Location { get; set; }


    }
}
