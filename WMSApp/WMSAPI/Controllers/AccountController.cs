using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMSAPI.ViewModels;
using WMS.Data;
using Application.Services.Master;
using AutoMapper;
using Domain.Model.Masters;
using WMS.Web.Framework.Infrastructure.Extentsion;
using Application.Services;
using System.Linq;
using System;
using System.Collections.Generic;
using WMSAPI.ViewModels.Account;
using Application.Services.Security;
using Microsoft.Extensions.Logging;
using Sentry;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : BaseAdminController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        private readonly IBranchHelper _branchService;
        private readonly IWorkContext _workContext;
        private readonly IUserBranchMappingService _userBranchMappingService;
        private readonly IPermissionMasterService _permissionMasterServcie;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IUserProfileService userProfileService,
                              IMapper mapper,
                              IBranchHelper branchService,
                              IWorkContext workContext,
                              IUserBranchMappingService userBranchMappingService,
                              IPermissionMasterService permissionMasterServcie,
                              ILogger<AccountController> logger,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userProfileService = userProfileService;
            _mapper = mapper;
            _branchService = branchService;
            _workContext = workContext;
            _userBranchMappingService = userBranchMappingService;
            _permissionMasterServcie = permissionMasterServcie;
            _logger = logger;
            _configuration = configuration;
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.User, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, model.Role);
                    UserProfile profile = _mapper.Map<UserProfile>(model);
                    profile.UserId = user.Id;
                    profile.CreateOn = DateTime.Now;
                    profile.BranchId = model.BranchList.FirstOrDefault();
                    _userProfileService.Insert(profile);
                    List<UserBranchMapping> userBranch = new List<UserBranchMapping>();
                    foreach (var item in model.BranchList)
                    {
                        var branch = new UserBranchMapping()
                        {
                            BranchId = item,
                            RefGuid = user.Id,

                        };
                        userBranch.Add(branch);
                    }
                    _userBranchMappingService.Insert(userBranch);
                    SuccessNotification("New User created successfully.");
                    return RedirectToAction("List", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            model.Branches = _branchService.GetAllBranches().ToList();
            return Json(model);
        }






        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    var loggedinUser = await _userManager.FindByEmailAsync(user.Email);
                    var userProfile = _userProfileService.GetByUserId(loggedinUser.Id);
                    //if (userProfile != null)
                    //{
                    //    _workContext.SetLoginBranch(userProfile.BranchId);
                    //}
                    //else
                    //{

                    //}
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userProfile.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, userProfile.UserId),
                    new Claim("name", userProfile.FirstName+" "+userProfile.LastName),
                    new Claim("id", userProfile.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    var token = GetToken(authClaims);

                    return Ok(new
                    {
                        status = true,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,

                    });
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return Ok(new { error = ModelState });
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.User, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }
            var userProfile = _userProfileService.GetById(id);
            var user = await _userManager.FindByIdAsync(userProfile.UserId);
            var role = await _userManager.GetRolesAsync(user);
            UpdateUserModel model = new UpdateUserModel()
            {
                Address = userProfile.Address,
                BranchId = userProfile.BranchId,
                City = userProfile.City,
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                PinCode = userProfile.PinCode,
                State = userProfile.State,
                Role = role.FirstOrDefault(),
                UserId = userProfile.UserId
            };

            var branch = _branchService.GetAllBranch();
            model.Branches = branch;
            model.BranchList = _userBranchMappingService.GetBranchMapping(userProfile.UserId).Select(x => x.BranchId).ToList();

            return View(model);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateUserModel model)
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.User, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }
            if (!ModelState.IsValid)
            {


                var branch = _branchService.GetAllBranch();
                model.Branches = branch;
                model.BranchList = _userBranchMappingService.GetBranchMapping(model.UserId).Select(x => x.BranchId).ToList();

                return View(model);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                // check and update passowrd
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordRestStatus = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (passwordRestStatus.Succeeded)
                    {
                        SuccessNotification("Password successfully changed.");
                    }
                    else
                    {

                        ErrorNotification("Error in password change.");
                    }
                }

                var userProfile = _userProfileService.GetById(model.Id);
                userProfile.Address = model.Address;
                userProfile.FirstName = model.FirstName;
                userProfile.LastName = model.LastName;
                userProfile.PinCode = model.PinCode;
                userProfile.State = model.State;
                userProfile.City = model.City;
                _userProfileService.Update(userProfile);

                _userBranchMappingService.DeleteBranch(userProfile.UserId);
                List<UserBranchMapping> userBranch = new List<UserBranchMapping>();
                foreach (var item in model.BranchList)
                {
                    var updateBranch = new UserBranchMapping()
                    {
                        BranchId = item,
                        RefGuid = userProfile.UserId,

                    };
                    userBranch.Add(updateBranch);
                }
                _userBranchMappingService.Insert(userBranch);
                SuccessNotification("user updated successfully.");
                return RedirectToAction("List");
            }

        }

        [HttpGet("BranchLogin")]
        public async Task<IActionResult> BranchLogin()
        {
            BranchLogin model = new BranchLogin();
            var userid = await _workContext.GetCurrentUserAsync();
            var branchs = _userBranchMappingService.GetBranchByUser(userid.Id);
            ViewBag.branch = branchs;
            return View(model);
        }

        [HttpPost("BranchLogin")]
        public IActionResult BranchLogin(BranchLogin model)
        {
            SentrySdk.CaptureMessage("branch login " + model.Id);

            _workContext.SetLoginBranch(model.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }





        [HttpPost("List")]
        public IActionResult List(DataSourceRequest request)
        {
            if (!_permissionMasterServcie.Authorize(StandardPermissionProvider.User, PermissionType.View).Result)
            {
                return AccessDeniedView();
            }
            var users = _userProfileService.GetAllProfile("");
            var gridData = new DataSourceResult()
            {

                Data = users.Select(x =>
                {
                    RegisterViewModel profile = _mapper.Map<RegisterViewModel>(x);
                    profile.Branch = x.Branch.BranchName;
                    return profile;
                }),
                Total = users.TotalCount
            };
            return Json(gridData);
        }

        protected JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
