using System;
using System.Collections.Generic;
using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyHelper _companyHelper;
        private readonly IItemHelper _itemHelper;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyHelper companyHelper, IMapper mapper, IItemHelper itemHelper)
        {
            _companyHelper = companyHelper;
            _mapper = mapper;
            _itemHelper = itemHelper;
        }

        // GET: CompaniesController
        public ActionResult Index()
        {
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
        [NonAction]
        public ActionResult Details(int id)
        {
            var c = new Company()
            {
                CompanyName = "Test",
                CompanyCode = "Test",
                Address = "Test",
                Location = "Location",
                ContactPersonHO = "ContactPersonHO",
                ContactNumberHO = "ContactNumberHO"
            ,
                EmailIdHO = "EmailIdHO",
                SpaceSizeFormat = "SpaceSizeFormat",
                Items = 1,
                SubItem = 1
            };
            return View(c);
        }

        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            Company company = new Company();
            var itemData = _itemHelper.GetAllItem();
            company.ItemList = _mapper.Map<List<Item>>(itemData);
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
            var c = new Company();
            try
            {
                var data = _companyHelper.GetCompanyById(id);
                c = _mapper.Map<Company>(data);
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
