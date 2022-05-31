using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Masters
{
    public class UserProfile : BaseEntity
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Email { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public int BranchId { get; set; }
        public virtual Branch Branch  { get; set; }


    }
}
