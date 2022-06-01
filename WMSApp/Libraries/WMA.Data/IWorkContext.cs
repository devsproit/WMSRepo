using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Masters;
using Domain.Model;
namespace WMS.Data
{
    public interface IWorkContext
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<UserProfile> UserProfile();
        void SetLoginBranch(int branchId);

        Task<Branch> GetCurrentBranch();
    }
}
