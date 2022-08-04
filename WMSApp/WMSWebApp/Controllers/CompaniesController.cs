using System;
using System.Collections.Generic;
using System.Net.Http;
using Application.Services;
using AutoMapper;
using Castle.Core.Configuration;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WMSWebApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Domain.Model.CompanyMaster;
using System.Linq;
using Application.Services.Security;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class CompaniesController : BaseAdminController
    {
        private readonly ICompanyHelper _companyHelper;
        private readonly IItemHelper _itemHelper;
        private readonly IMapper _mapper;
        private Microsoft.Extensions.Configuration.IConfiguration Configuration;
        private readonly IPermissionMasterService _permissionMasterService;
        public CompaniesController(ICompanyHelper companyHelper, IMapper mapper, IItemHelper itemHelper, Microsoft.Extensions.Configuration.IConfiguration _configuration, IPermissionMasterService permissionMasterService)
        {
            _companyHelper = companyHelper;
            _mapper = mapper;
            _itemHelper = itemHelper;
            Configuration = _configuration;
            _permissionMasterService = permissionMasterService;
        }

        // GET: CompaniesController
        public ActionResult Index()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Company, PermissionType.View).Result)
            {
                return AccessDeniedView();
            }
            List<Company> companies = new List<Company>();
            try
            {
                var data = _companyHelper.GetAllCompanies();
                companies = _mapper.Map<List<Company>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(companies);
        }

        // GET: CompaniesController/Details/5
        //[NonAction]
        //public ActionResult Details(int id)
        //{
        //    var c = new Company()
        //    {
        //        CompanyName = "Test",
        //        CompanyCode = "Test",
        //        Address = "Test",
        //        Location = "Location",
        //        ContactPersonHO = "ContactPersonHO",
        //        ContactNumberHO = "ContactNumberHO"
        //    ,
        //        EmailIdHO = "EmailIdHO",
        //        SpaceSizeFormat = "SpaceSizeFormat",
        //        Items = 1,
        //        SubItem = 1
        //    };
        //    return View(c);
        //}

        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Company, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            Company company = new Company();
            var itemData = _itemHelper.GetAllItem();
            company.ItemList = _mapper.Map<List<Item>>(itemData);
            Root districtslist = new Root();
            HttpClient client = new HttpClient();
            string apiUrl = Configuration.GetValue<string>("districtUrl");
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                districtslist = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
            }
            ViewBag.districts = districtslist.districts;
            return View(company);
        }

        // POST: CompaniesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company c, IFormCollection collection)
        {
            try
            {
                var company = _mapper.Map<CompanyDb>(c);

                foreach (var item in c.Items)
                {
                    CompanyItemsMapping itemMapping = new CompanyItemsMapping
                    {
                        Company = company,
                        ItemId = item,
                        CompanyId = company.Id
                    };
                    company.CompanyItemsMappings.Add(itemMapping);
                }

                _companyHelper.Insert(company);
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
            var c = new Company();
            try
            {
               
                var data = _companyHelper.GetById(id);
                c = _mapper.Map<Company>(data);
                var itemData = _itemHelper.GetAllItem();
                c.ItemList = _mapper.Map<List<Item>>(itemData);
                c.Items = data.CompanyItemsMappings.ToList().Select(x => x.ItemId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }

        // POST: CompaniesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Company c, IFormCollection collection)
        {
            try
            {
                var company = _mapper.Map<CompanyDb>(c);
                var b = _companyHelper.UpdateCompanyById(company);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.Company, PermissionType.Delete).Result)
            {
                return AccessDeniedView();
            }
            try
            {
                var b = _companyHelper.DeleteCompanyById(id);
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

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
    }
}
