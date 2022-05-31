
using Domain.Model.Masters;
using WMS.Core;

namespace Application.Services.Master
{
    public interface IUserProfileService
    {
        void Insert(UserProfile entity);
        void Update(UserProfile entity);
        UserProfile GetById(int id);
        UserProfile GetByUserId(string userId);
        IPagedList<UserProfile> GetAllProfile(string email, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
