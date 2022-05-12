using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;

namespace WMSWebApp.Controllers;
public class WarehouseController : Controller
{
    static List<Warehouse> _warehouseList = new List<Warehouse>();

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
}
