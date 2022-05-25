using Domain.Model;
using System.Collections.Generic;

namespace WMSWebApp.ViewModels
{
    public class IntransitViewModel
    {
        public Intrasitc intrasitc { get; set; }

        public List<Intrasitc> intrasitcList { get; set; }

        public IEnumerable<BranchDb> listSenderBranch { get; set; }

        public IEnumerable<CompanyDb> listSenderCompany { get; set; }

        public int CId { get; set; }

        public int BId { get; set; }

        public int ItId { get; set; }

        public int SubItemId { get; set; }
    }
}
