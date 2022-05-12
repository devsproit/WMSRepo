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
    public class SubItemController : Controller
    {
        private readonly ISubItemHelper _SubItemHelper;
        private readonly IMapper _mapper;


        public SubItemController(ISubItemHelper SubItemHelper, IMapper mapper)
        {
            _SubItemHelper = SubItemHelper;
            _mapper = mapper;
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
            catch (Exception)
            {
                throw;
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
                ItemName = "Test",
                MaterialDescription = "Test",
                SubItemSize="Test",
                FOC="Test",
                SubItemCategory = "Test",
                SubItemSR = "Test",

            };
            return View(B);
        }
        // GET: SubItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubItem c, IFormCollection collection)
        {
            try
            {
                var SubItem = _mapper.Map<SubItemDb>(c);
                var b = _SubItemHelper.CreateNewSubItem(SubItem);
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
    }
}

