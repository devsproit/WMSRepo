using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WMS.Data;
using Domain.Model.Masters;
using Application.Services.Master;
namespace WMS.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileService _userProfileService;
        #endregion

        #region Ctor
        public WebWorkContext(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserProfileService userProfileService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userProfileService = userProfileService;
        }
        #endregion

        #region methods
        public virtual Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public virtual async Task<UserProfile> UserProfile()
        {
            var user = await GetCurrentUserAsync();
            var profile = _userProfileService.GetByUserId(user.Id);
            return profile;
        }
        #endregion

        #region Properties

        #endregion
    }
}