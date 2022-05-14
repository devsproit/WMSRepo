using Domain.Model;
using System.Collections.Generic;

namespace Application.Services
{

    public interface IBranchHelper
    {
        bool DeleteBranchById(int Id);
        List<BranchDb> GetAllBranch();
        BranchDb GetBranchById(int Id);
        bool UpdateBranchById(BranchDb Branch);
        bool CreateNewBranch(BranchDb Branch);
        void Insert(BranchDb entity);
    }
}