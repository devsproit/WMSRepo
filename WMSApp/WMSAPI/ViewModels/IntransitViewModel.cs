using Domain.Model;
using System.Collections.Generic;

namespace WMSAPI.ViewModels
{
    public class IntransitViewModel
    {
        public Intrasitc intrasitc { get; set; }

        public List<Intrasitc> intrasitcList { get; set; }

        public IEnumerable<Branch> listSenderBranch { get; set; }

        public IEnumerable<CompanyDb> listSenderCompany { get; set; }

        public int CId { get; set; }

        public int BId { get; set; }

        public int ItId { get; set; }

        public int SubItemId { get; set; }
    }
}
