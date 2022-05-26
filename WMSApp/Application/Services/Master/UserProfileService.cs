using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.Masters;
namespace Application.Services.Master
{
    public partial class UserProfileService : IUserProfileService
    {
        #region Fields
        private readonly IRepository<UserProfile> _userProfileRepository;

        #endregion

        #region Ctor

        public UserProfileService(IRepository<UserProfile> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(UserProfile entity)
        {
            _userProfileRepository.Insert(entity);
        }

        public virtual void Update(UserProfile entity)
        {
            _userProfileRepository.Update(entity);
        }

        public virtual UserProfile GetById(int id)
        {
            return _userProfileRepository.GetById(id);
        }

        public virtual UserProfile GetByUserId(string userId)
        {
            var query = from x in _userProfileRepository.Table
                        where x.UserId == userId
                        select x;
            return query.FirstOrDefault();
        }
        #endregion
    }
}
