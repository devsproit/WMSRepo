using Application.Services;
using Application.Services.Master;
using Domain.Model.SubItemMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels.SubItemMapping;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class SubItemMappingController : BaseAdminController
    {
        #region Fields
        private readonly ISubItemWarehouseMappingService _subItemWarehouseMappingService;
        private readonly ISubItemHelper _subItemService;
        #endregion

        #region Ctor
        public SubItemMappingController(ISubItemWarehouseMappingService subItemWarehouseMappingService, ISubItemHelper subItemService)
        {
            _subItemWarehouseMappingService = subItemWarehouseMappingService;
            _subItemService = subItemService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View();
        }

        [HttpPost]
        public IActionResult List(DataSourceRequest request, string subItemName = "")
        {
            return View();
        }

        public IActionResult Create()
        {
            SubItemMappingModel model = new SubItemMappingModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SubItemMappingModel model)
        {
            if (ModelState.IsValid)
            {
                SubItemWareHouseMapping mapping = new SubItemWareHouseMapping();
                mapping.WareHouseAreaId = model.WareHouseAreaId;
                mapping.LocationId = model.LocationId;
                mapping.SubItemCode = model.SubItemCode;
                mapping.WareHouseId = model.WareHouseId;
                mapping.CreateOn = DateTime.Now;
                _subItemWarehouseMappingService.Insert(mapping);
                SuccessNotification("Subitem successfully mapped");
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }



        [HttpGet]
        public IActionResult GetSubItem()
        {
            var subitems = _subItemService.GetSubItem("0");
            List<SubItemListModel> list = new List<SubItemListModel>();
            foreach (var item in subitems)
            {
                SubItemListModel model = new SubItemListModel();
                model.Name = $"{item.SubItemName} ({item.SubItemCode})";
                model.Code = item.SubItemCode;
                list.Add(model);
            }
            return Json(list);
        }
        #endregion



    }
}
