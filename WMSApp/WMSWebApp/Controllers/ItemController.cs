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
using Application.Services.Security;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class ItemController : BaseAdminController
    {
        private readonly IItemHelper _ItemHelper;
        private readonly IMapper _mapper;
        private readonly ICompanyHelper _companyService;
        private readonly IPermissionMasterService _permissionMasterService;
        public ItemController(IItemHelper ItemHelper, IMapper mapper, ICompanyHelper companyService, IPermissionMasterService permissionMasterService)
        {
            _ItemHelper = ItemHelper;
            _mapper = mapper;
            _companyService = companyService;
            _permissionMasterService = permissionMasterService;
        }


        // GET: ItemController
        public ActionResult Index()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.ItemMaster, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
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
              
                ItemName = "Test",
                ItemCode = "Test",

            };
            return View(B);
        }
        // GET: ItemController/Create
        public ActionResult Create()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.ItemMaster, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            Item model = new Item();
            //model.Companies = _companyService.GetAllCOmpany().ToList();
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
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.ItemMaster, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }
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
            //model.Companies = _companyService.GetAllCOmpany().ToList();
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
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.ItemMaster, PermissionType.Delete).Result)
            {
                return AccessDeniedView();
            }
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

