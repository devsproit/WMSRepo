
using Domain.Model.Masters;
namespace Application.Services.Master
{
    public interface IUserProfileService
    {
        void Insert(UserProfile entity);
        void Update(UserProfile entity);
        UserProfile GetById(int id);
        UserProfile GetByUserId(string userId);
    }
}
