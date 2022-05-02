using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Controllers
{
    public class BranchController : Controller
    {

        // GET: BranchController
        public IActionResult Index()
        {
            List<Branch> Branch = new List<Branch>();
            Branch.Add(new Branch()
            {
                BranchName = "Test",
                BranchCode = "Test",
                CompanyName = "Test",
                Address = "Test",
                Location = "Location",
                ContactPersonBranch = "ContactPersonBranch",
                ContactNumberBranch = "ContactNumberHO"
            ,
                EmailIdBranch = "EmailIdHO",
                AssociatedEmployee = "AssociatedEmployee ",
                WarehouseName = "WH01",
               
            });
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: CompaniesController/Edit/5
        public ActionResult Edit(int id)
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
        // POST: CompaniesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        // GET: CompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchController/Delete/5
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
