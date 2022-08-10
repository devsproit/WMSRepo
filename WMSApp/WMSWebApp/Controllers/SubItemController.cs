using System;
using System.Collections.Generic;
using Application.Services;
using Application.Services.Security;
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
    public class SubItemController : BaseAdminController
    {
        private readonly ISubItemHelper _SubItemHelper;
        private readonly IMapper _mapper;
        private readonly IItemHelper _itemService;
        private readonly IPermissionMasterService _permissionMasterService;


        public SubItemController(ISubItemHelper SubItemHelper, IMapper mapper, IItemHelper itemService, IPermissionMasterService permissionMasterService)
        {
            _SubItemHelper = SubItemHelper;
            _mapper = mapper;
            _itemService = itemService;
            _permissionMasterService = permissionMasterService;
        }


        // GET: SubItemController
        public ActionResult Index()
        {
            List<SubItem> SubItem = new List<SubItem>();
            try
            {
                var data = _SubItemHelper.GetAllSubItem();
                SubItem = _mapper.Map<List<SubItem>>(data);
            }
            catch (Exception ex)
            {

            }
            return View(SubItem);
        }
        // GET: SubItemController/Details/5
        public ActionResult Details(int id)
        {
            var B = new SubItem()
            {
                SubItemCode = "Test",
                SubItemName = "Test",
                ItemId = 1,
                MaterialDescription = "Test",
                SubItemSize = "Test",
                FOC = false,
                SubItemCategory = "Test",
                SubItemSR = "Test",

            };
            return View(B);
        }
        // GET: SubItemController/Create
        public ActionResult Create()
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.SubItemMaster, PermissionType.Add).Result)
            {
                return AccessDeniedView();
            }
            SubItem model = new SubItem();
            var items = _itemService.GetAllItem();
            model.Items = _mapper.Map<List<Item>>(items);
            return View(model);
        }

        // POST: SubItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubItem c, IFormCollection collection)
        {
            try
            {
                var SubItem = _mapper.Map<SubItemDb>(c);
                _SubItemHelper.Insert(SubItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: SubItemController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.SubItemMaster, PermissionType.Edit).Result)
            {
                return AccessDeniedView();
            }
            var c = new SubItem();
            try
            {
                var data = _SubItemHelper.GetSubItemById(id);
                c = _mapper.Map<SubItem>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // POST: SubItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubItem c, IFormCollection collection)
        {
            try
            {
                var SubItem = _mapper.Map<SubItemDb>(c);
                var b = _SubItemHelper.UpdateSubItemById(SubItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: SubItemController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_permissionMasterService.Authorize(StandardPermissionProvider.SubItemMaster, PermissionType.Delete).Result)
            {
                return AccessDeniedView();
            }
            return View();
        }

        // POST: SubItemController/Delete/5

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

        #region APis


        [HttpGet]
        public IActionResult SearchSubItem(int itemId)
        {
            List<SubItem> subItems = new List<SubItem>();
            var subItemsData = _SubItemHelper.GetSubItemByItemId(itemId);
            subItems = _mapper.Map<List<SubItem>>(subItemsData);
            return Json(subItems);
        }


        #endregion
    }
}

