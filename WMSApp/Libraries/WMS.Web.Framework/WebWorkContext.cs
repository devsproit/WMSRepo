using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WMS.Data;
using Domain.Model;
using Domain.Model.Masters;
using Application.Services.Master;
using Application.Services;
using WMS.Core.Http;
using Sentry;
namespace WMS.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileService _userProfileService;
        private readonly IBranchHelper _branchService;

        #endregion

        #region Ctor
        public WebWorkContext(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserProfileService userProfileService, IBranchHelper branchService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userProfileService = userProfileService;
            _branchService = branchService;
        }

        #endregion

        #region methods

        protected virtual string GetCustomerCookie(string cookieName)
        {
            //var cookieName = $"{WmsCookieDefaults.Prefix}{WmsCookieDefaults.CustomerCookie}";
            return _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response?.HasStarted ?? true)
                return;

            //delete current cookie value
            var cookieName = $"{WmsCookieDefaults.Prefix}{WmsCookieDefaults.CustomerCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24 * 365;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (customerGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = true
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, customerGuid.ToString(), options);
        }
        protected virtual void SetBranchCookie(int branchId)
        {
            SentrySdk.CaptureMessage("Set Cookies branch login " + branchId);
            if (_httpContextAccessor.HttpContext?.Response?.HasStarted ?? true)
                return;

            //delete current cookie value
            var cookieName = $"{WmsCookieDefaults.Prefix}{WmsCookieDefaults.CustomerCookie}{WmsCookieDefaults.BranchId}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24 * 365;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (branchId == 0)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = true,
                SameSite = SameSiteMode.Strict

            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, branchId.ToString(), options);
        }
        public virtual Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public virtual async Task<UserProfile> UserProfile()
        {
            var user = await GetCurrentUserAsync();
            var profile = _userProfileService.GetByUserId(user.Id);
            return profile;
        }

        public virtual void SetLoginBranch(int branchId)
        {
            SetBranchCookie(branchId);
        }

        public virtual async Task<Branch> GetCurrentBranch()
        {
            Branch branch = new Branch();
            var cookieName = $"{WmsCookieDefaults.Prefix}{WmsCookieDefaults.CustomerCookie}{WmsCookieDefaults.BranchId}";
            var branchid = GetCustomerCookie(cookieName);
            if (branchid == null)
            {
                var user = await UserProfile();
                SentrySdk.CaptureMessage("GetCurrentBranch " + user.BranchId);
                SetBranchCookie(user.BranchId);
                branch = _branchService.GetById(user.BranchId);
            }
            else
            {
                branch = _branchService.GetById(Convert.ToInt32(branchid));
            }

            return branch;

        }

        #endregion

        #region Properties

        #endregion
    }
}