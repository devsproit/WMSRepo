using System;
using System.Collections.Generic;
using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMS.Data;
using WMSWebApp.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Master;
using Domain.Model.Masters;
using Application.Services.WarehouseMaster;
using WMS.Web.Framework.Infrastructure.Extentsion;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Application.Services.Security;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class BranchController : BaseAdminController
    {
        private readonly IBranchHelper _BranchHelper;
        private readonly IMapper _mapper;
        private readonly ICompanyHelper _companyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserProfileService _userProfileService;
        private readonly IWarehouseService _warehouseService;
        private readonly IConfiguration Configuration;
        private readonly IPermissionMasterService _permissionMasterService;

        public BranchController(IBranchHelper BranchHelper, IMapper mapper, ICompanyHelper companyService, UserManager<ApplicationUser> userManager,
            IUserProfileService userProfileService, IWarehouseService warehouseService, IConfiguration _configuration, IPermissionMasterService permissionMasterService)
        {
            _BranchHelper = BranchHelper;
            _mapper = mapper;
            _companyService = companyService;
            _userManager = userManager;
            _userProfileService = userProfileService;
            _warehouseService = warehouseService;
            Configuration = _configuration;
            _permissionMasterService = permissionMasterService;
        }



        public ActionResult Index()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Branch, PermissionType.View).Result)
            {
                return AccessDeniedView();
            }
            return View();
        }


        [HttpPost]
        public virtual IActionResult List(DataSourceRequest request)
        {
            var branches = _BranchHelper.GetAllBranches(request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult
            {
                Data = branches.Select(x =>
                {
                    BranchModel m = new BranchModel();
                    m = _mapper.Map<BranchModel>(x);
                    var user = _userProfileService.GetByUserId(x.AssociatedEmployee);
                    if (user != null)
                    {
                        m.UserName = $"{user.FirstName} {user.LastName}";
                    }
                    var warehouse = x.BranchWiseWarehouses.Select(x => x.Warehouse);

                    if (warehouse.Any())
                    {
                        m.Werehouse = string.Join(',', warehouse.Select(x => x.WarehouseName).ToArray());
                    }
                    var comp = _companyService.GetCompanyById(x.CompanyId);
                    if (comp != null)
                    {
                        m.CompanyName = comp.CompanyName;
                    }
                    return m;
                }),
                Total = branches.TotalCount
            };
            return Json(gridData);
        }

        public ActionResult Details(int id)
        {
            var B = new Branch()
            {
                BranchName = "Test",
                BranchCode = "Test",
                CompanyId = 1,
                Address = "Test",
                Location = "Location",
                ContactPersonBranch = "ContactPersonBranch",
                ContactNumberBranch = "ContactNumberBranch"
            ,
                EmailIdBranch = "EmailIdBranch",
                AssociatedEmployee = "AssociatedEmployee ",
                //WarehouseId = 1,
            };
            return View(B);
        }
        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Branch, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            BranchModel model = new BranchModel();
            var comp = _companyService.GetAllCompanies();
            var companies = _mapper.Map<List<Company>>(comp);
            model.Companies = companies;
            var users = PrepareUserList(_userProfileService.GetAllProfile("").ToList());
            model.Users = users;
            model.Warehouses = _warehouseService.GetAllWarehouse().ToList();
            Root districtslist = new Root();
            HttpClient client = new HttpClient();
            string apiUrl = Configuration.GetValue<string>("districtUrl");
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                districtslist = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
            }
            ViewBag.districts = districtslist.districts;
            return View(model);
        }

        // POST: BranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BranchModel c, IFormCollection collection)
        {
            try
            {
                var branch = _mapper.Map<Branch>(c);
                foreach (var item in c.WarehouseId)
                {
                    var mapping = new BranchWiseWarehouse()
                    {
                        WarehouseId = item,
                        Branch = branch,



                    };
                    branch.BranchWiseWarehouses.Add(mapping);
                }
                _BranchHelper.Insert(branch);
                SuccessNotification("Branch created successfully!.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: CompaniesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Company, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }
            var model = new BranchModel();
            try
            {
                var data = _BranchHelper.GetById(id);
                model = _mapper.Map<BranchModel>(data);
                var comp = _companyService.GetAllCompanies();
                var companies = _mapper.Map<List<Company>>(comp);
                model.Companies = companies;
                var users = PrepareUserList(_userProfileService.GetAllProfile("").ToList());
                model.Users = users;
                model.Warehouses = _warehouseService.GetAllWarehouse().ToList();
                model.WarehouseId = data.BranchWiseWarehouses.Select(x => x.WarehouseId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        // POST: CompaniesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BranchModel c, IFormCollection collection)
        {
            var branch = _BranchHelper.GetById(c.Id);
            branch = _mapper.Map<BranchModel, Branch>(c, branch);

           
            _BranchHelper.Update(branch);
            SuccessNotification("Branch updated successfully!.");
            return RedirectToAction(nameof(Index));
        }
        // GET: CompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchController/Delete/5
        // POST: CompaniesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        protected List<UserProfileModel> PrepareUserList(List<UserProfile> users)
        {
            List<UserProfileModel> list = new List<UserProfileModel>();
            foreach (var item in users)
            {
                UserProfileModel model = new UserProfileModel();
                model.UserId = item.UserId;
                model.Name = $"{item.FirstName} {item.LastName}";
                list.Add(model);
            }
            return list;
        }

        protected void PrepareBrachCreate(Branch branch, BranchModel model)
        {
            branch.Address = model.Address;
            branch.BranchCode = model.BranchCode;
            branch.BranchName = model.BranchName;
            branch.CompanyId = model.CompanyId;
            branch.EmailIdBranch = model.EmailIdBranch;

        }
    }
}
