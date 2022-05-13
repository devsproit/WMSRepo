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
    public class ItemController : Controller
    {
        private readonly IItemHelper _ItemHelper;
        private readonly IMapper _mapper;


        public ItemController(IItemHelper ItemHelper, IMapper mapper)
        {
            _ItemHelper = ItemHelper;
            _mapper = mapper;
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
                CompanyName = "Test",
                ItemName = "Test",
                ItemCode = "Test",

            };
            return View(B);
        }
        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
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
            var c = new Item();
            try
            {
                var data = _ItemHelper.GetItemById(id);
                c = _mapper.Map<Item>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Item c, IFormCollection collection)
        {
            try
            {
                var Item = _mapper.Map<ItemDb>(c);
                var b = _ItemHelper.UpdateItemById(Item);
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

