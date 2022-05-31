using Domain.Model;
using System.Collections.Generic;
using WMS.Core;
namespace Application.Services
{

    public interface IBranchHelper
    {
        bool DeleteBranchById(int Id);
        List<Branch> GetAllBranch();
        Branch GetBranchById(int Id);
        bool UpdateBranchById(Branch Branch);
        bool CreateNewBranch(Branch Branch);
        void Insert(Branch entity);
        IPagedList<Branch> GetAllBranches(int pageIndex = 0, int pageSize = int.MaxValue);
        Branch GetById(int id);
    }
}