using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: CompaniesController
        public ActionResult Index()
        {
            List<Company> companies = new List<Company>();
            companies.Add(new Company() { CompanyName = "Test", CompanyCode = "Test", Address = "Test", Location = "Location", ContactPersonHO = "ContactPersonHO", ContactNumberHO = "ContactNumberHO" 
            ,EmailIdHO= "EmailIdHO",
                SpaceSizeFormat= "SpaceSizeFormat",
                Items = "Items",
                SubItem = "SubItem"
            });
            return View(companies);
        }

        // GET: CompaniesController/Details/5
        public ActionResult Details(int id)
        {
            var c= new Company()
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
                Items = "Items",
                SubItem = "SubItem"
            };
            return View(c);
        }

        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompaniesController/Create
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
                Items = "Items",
                SubItem = "SubItem"
            };
            return View(c);
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
