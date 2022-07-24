using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;
using Application.Services.WarehouseMaster;
using AutoMapper;
using Domain.Model.Masters;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace WMSWebApp.Controllers;
[Authorize]
public class WarehouseController : Controller
{

    #region fields
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;
    private readonly IConfiguration Configuration;
    #endregion

    #region Ctor
    public WarehouseController(IWarehouseService warehouseService, IMapper mapper, IConfiguration _configuration)
    {
        _warehouseService = warehouseService;
        _mapper = mapper;
        Configuration = _configuration;
    }
    #endregion


    #region methdos
    static List<WarehouseModel> _warehouseList = new List<WarehouseModel>();

    // GET: WarehouseController
    public ActionResult Index()
    {
        var warehouses = _warehouseService.GetAllWarehouse(0, 20).ToList();
        _warehouseList = _mapper.Map<List<WarehouseModel>>(warehouses);
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
        Root districtslist = new Root();
        HttpClient client = new HttpClient();
        string apiUrl = Configuration.GetValue<string>("districtUrl");
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;
        if (response.IsSuccessStatusCode)
        {
            districtslist = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
        }
        ViewBag.districts = districtslist.districts;
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
                var area = new WarehouseZone()
                {

                    Warehouse = warehouse,
                    ZoneCode = zoneArea.ZoneCode,
                    ZoneName = zoneArea.ZoneName
                };
                warehouse.WarehouseZones.Add(area);
            }
            int warehouseId = _warehouseService.Insert(warehouse);
            return RedirectToAction("CreateArea", new { warehouseid = warehouseId });
        }
        catch
        {
            return View();
        }
    }

    public IActionResult CreateArea(int warehouseid)
    {
        var warehouse = _warehouseService.GetById(warehouseid);
        if (warehouse == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            WarehouseAreaList model = new WarehouseAreaList();
            //WarehouseArea model = new WarehouseArea();
            model.WarehouseId = warehouseid;
            //model.AvailableArea = warehouse.TotalSpaceSize;
            ////model.ZoneList = _warehouseService.GetAllZone(warehouseid, 0, int.MaxValue).ToList();
            //areaList.WarehouseAreas.Add(model);

            ViewBag.AvailableSize = warehouse.TotalSpaceSize;
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult GetZone(int warehouseid)
    {
        var zone = _warehouseService.GetAllZone(warehouseid, 0, int.MaxValue).ToList();
        List<WarehouseZoneAreaModel> models = _mapper.Map<List<WarehouseZoneAreaModel>>(zone);

        return Json(models);

    }

    [HttpPost]
    public IActionResult CreateArea(WarehouseAreaList model)
    {
        var areas = _mapper.Map<List<WarehouseZoneArea>>(model.WarehouseAreas);
        foreach (var item in areas)
        {
            item.WarehouseId = model.WarehouseId;
        }
        _warehouseService.InsertArea(areas);
        return RedirectToAction("Index");
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
