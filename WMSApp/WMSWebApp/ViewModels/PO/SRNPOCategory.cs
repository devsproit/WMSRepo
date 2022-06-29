namespace WMSWebApp.ViewModels.PO
{
    public class SRNPOCategory
    {
        public string SrnSerialNumber { get; set; }
        public string SrnPOCategory { get; set; }
        public string SrnPOSendingTo { get; set; }

        public string SrnPOItem { get; set; }
        public string SrnPOSubItem { get; set; }

        public int SrnPOQty { get; set; }
        public string SrnPOSrnCause { get; set; }


    }
}