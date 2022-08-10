using Domain.Model;
using Domain.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Master
{
    public partial interface IUserBranchMappingService
    {
        void Insert(UserBranchMapping entity);
        void Update(UserBranchMapping entity);
        List<Branch> GetBranchByUser(string refguid);
        void Insert(List<UserBranchMapping> entity);
        List<UserBranchMapping> GetBranchMapping(string userId, int pageIndex = 0, int pageSize = int.MaxValue);
        void DeleteBranch(string userId);

    }
}
