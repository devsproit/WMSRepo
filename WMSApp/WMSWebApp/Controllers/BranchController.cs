using System;
using System.Collections.Generic;
using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;


namespace WMSWebApp.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchHelper _BranchHelper;
        private readonly IMapper _mapper;


        public BranchController(IBranchHelper BranchHelper, IMapper mapper)
        {
            _BranchHelper = BranchHelper;
            _mapper = mapper;
        }


        // GET: BranchController
        public ActionResult Index()
        {
            List<Branch> Branch = new List<Branch>();
            try
            {
                var data = _BranchHelper.GetAllBranch();
                Branch = _mapper.Map<List<Branch>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Branch);
        }
        // GET: BranchController/Details/5
        public ActionResult Details(int id)
        {
            var B = new Branch()
            {
                BranchName = "Test",
                BranchCode = "Test",
                CompanyName = "Test",
                Address = "Test",
                Location = "Location",
                ContactPersonBranch = "ContactPersonBranch",
                ContactNumberBranch = "ContactNumberBranch"
            ,
                EmailIdBranch = "EmailIdBranch",
                AssociatedEmployee = "AssociatedEmployee ",
                WarehouseName = "WH01",
            };
            return View(B);
        }
        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch c, IFormCollection collection)
        {
            try
            {
                var Branch = _mapper.Map<BranchDb>(c);
                var b = _BranchHelper.CreateNewBranch(Branch);
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
            var c = new Branch();
            try
            {
                var data = _BranchHelper.GetBranchById(id);
                c = _mapper.Map<Branch>(data);
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
        public ActionResult Edit(int id, Branch c, IFormCollection collection)
        {
            try
            {
                var Branch = _mapper.Map<BranchDb>(c);
                var b = _BranchHelper.UpdateBranchById(Branch);
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
    }
}
