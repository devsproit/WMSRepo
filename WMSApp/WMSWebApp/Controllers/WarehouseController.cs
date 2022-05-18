using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;
using Application.Services.WarehouseMaster;
using AutoMapper;
using Domain.Model.Masters;
namespace WMSWebApp.Controllers;
public class WarehouseController : Controller
{

    #region fields
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;
    #endregion

    #region Ctor
    public WarehouseController(IWarehouseService warehouseService, IMapper mapper)
    {
        _warehouseService = warehouseService;
        _mapper = mapper;
    }
    #endregion


    #region methdos
    static List<WarehouseModel> _warehouseList = new List<WarehouseModel>();

    // GET: WarehouseController
    public ActionResult Index()
    {
        return View(_warehouseList);
    }

    [NonAction]
    // GET: WarehouseController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: WarehouseController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: WarehouseController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(WarehouseModel model)
    {
        try
        {
            var warehouse = _mapper.Map<Warehouse>(model);
            foreach (var zoneArea in model.ZoneAreaList)
            {
                var area = new WarehouseZoneArea()
                {
                    AreaCode = zoneArea.AreaCode,
                    AreaName = zoneArea.AreaName,
                    AreaType = zoneArea.AreaType,
                    Size = zoneArea.Size,
                    Warehouse = warehouse,
                    ZoneCode = zoneArea.ZoneCode,
                    ZoneName = zoneArea.ZoneName
                };
            }
            _warehouseService.Insert(warehouse);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: WarehouseController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: WarehouseController/Edit/5
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

    // GET: WarehouseController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: WarehouseController/Delete/5
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
    #endregion


}
