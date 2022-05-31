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
using System.Linq;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class ItemController : BaseAdminController
    {
        private readonly IItemHelper _ItemHelper;
        private readonly IMapper _mapper;
        private readonly ICompanyHelper _companyService;

        public ItemController(IItemHelper ItemHelper, IMapper mapper, ICompanyHelper companyService)
        {
            _ItemHelper = ItemHelper;
            _mapper = mapper;
            _companyService = companyService;
        }


        // GET: ItemController
        public ActionResult Index()
        {
            List<Item> Item = new List<Item>();
            try
            {
                var data = _ItemHelper.GetAllItem();
                Item = _mapper.Map<List<Item>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Item);
        }
        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            var B = new Item()
            {
                CompanyId = 1,
                ItemName = "Test",
                ItemCode = "Test",

            };
            return View(B);
        }
        // GET: ItemController/Create
        public ActionResult Create()
        {
            Item model = new Item();
            model.Companies = _companyService.GetAllCOmpany().ToList();
            return View(model);
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item c, IFormCollection collection)
        {
            try
            {
                var Item = _mapper.Map<ItemDb>(c);
                var b = _ItemHelper.CreateNewItem(Item);
                SuccessNotification("Item Created.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new Item();
           
            try
            {
                var data = _ItemHelper.GetItemById(id);

                model = _mapper.Map<Item>(data);
                
            }
            catch (Exception)
            {
                throw;
            }
            model.Companies = _companyService.GetAllCOmpany().ToList();
            return View(model);
        }
        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Item c, IFormCollection collection)
        {
            try
            {
                var Item = _mapper.Map<ItemDb>(c);
                 _ItemHelper.Update(Item);
                SuccessNotification("Item Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemController/Delete/5

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

