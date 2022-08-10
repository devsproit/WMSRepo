using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.Masters;
using Domain.Model;

namespace Application.Services.Master
{
    public partial class UserBranchMappingService : IUserBranchMappingService
    {
        #region Fields
        private readonly IRepository<UserBranchMapping> _userBranchRepository;
        private readonly IRepository<Branch> _branchRepository;
        #endregion

        #region ctor

        public UserBranchMappingService(IRepository<UserBranchMapping> userBranchRepository, IRepository<Branch> branchRepository)
        {
            _userBranchRepository = userBranchRepository;
            _branchRepository = branchRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(UserBranchMapping entity)
        {
            _userBranchRepository.Insert(entity);
        }

        public virtual void Insert(List<UserBranchMapping> entity)
        {
            _userBranchRepository.Insert(entity);
        }
        public virtual void Update(UserBranchMapping entity)
        {
            _userBranchRepository.Update(entity);
        }


        public virtual List<Branch> GetBranchByUser(string refguid)
        {
            var query = from x in _userBranchRepository.Table
                        join
                         y in _branchRepository.Table on x.BranchId equals y.Id
                        where x.RefGuid == refguid
                        select y;
            return query.ToList();
        }

        public virtual List<UserBranchMapping> GetBranchMapping(string userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _userBranchRepository.Table
                        where x.RefGuid == userId
                        select x;
            query = query.OrderByDescending(x => x.Id);
            var result = new PagedList<UserBranchMapping>(query, pageIndex, pageSize);
            return result;
        }

        public virtual void DeleteBranch(string userId)
        {
            var query = from x in _userBranchRepository.Table
                        where x.RefGuid == userId
                        select x;
            _userBranchRepository.Delete(query);

        }
        #endregion
    }
}
