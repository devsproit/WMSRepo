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
    public class LocationController : Controller
    {
        private readonly ILocationHelper _LocationHelper;
        private readonly IMapper _mapper;


        public LocationController(ILocationHelper LocationHelper, IMapper mapper)
        {
            _LocationHelper = LocationHelper;
            _mapper = mapper;
        }


        // GET: LocationController
        public ActionResult Index()
        {
            List<Location> Location = new List<Location>();
            try
            {
                var data = _LocationHelper.GetAllLocation();
                Location = _mapper.Map<List<Location>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Location);
        }
        // GET: LocationController/Details/5
        public ActionResult Details(int id)
        {
            var B = new Location()
            {
                LocationName = "Test",
                LocationCode = "Test",
                
            };
            return View(B);
        }
        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location c, IFormCollection collection)
        {
            try
            {
                var Location = _mapper.Map<LocationDb>(c);
                var b = _LocationHelper.CreateNewLocation(Location);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var c = new Location();
            try
            {
                var data = _LocationHelper.GetLocationById(id);
                c = _mapper.Map<Location>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Location c, IFormCollection collection)
        {
            try
            {
                var Location = _mapper.Map<LocationDb>(c);
                var b = _LocationHelper.UpdateLocationById(Location);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        
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

