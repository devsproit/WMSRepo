using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WMSWebApp.Models;
using Application.Services;
using WMSWebApp.ViewModels.Dashboard;
using Application.Services.GRN;
using WMS.Data;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels;
using Domain.Model.GRN;
namespace WMSWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIntrasitService _intrasitService;
        private readonly IWorkContext _workContext;
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        public HomeController(ILogger<HomeController> logger, IIntrasitService intrasitService, IWorkContext workContext, IGoodReceivedNoteMasterService goodReceivedNoteMasterService)
        {
            _logger = logger;
            _intrasitService = intrasitService;
            _workContext = workContext;
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
        }

        public async Task<IActionResult> Index()
        {
            var branch = await _workContext.GetCurrentBranch();
            DashboardCountModel model = new DashboardCountModel();
            var pendingGRN = _intrasitService.GetPendingPO(branch.BranchCode, "0");
            model.GRN = pendingGRN.Count;
            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Stock()
        {
            var branch = await _workContext.GetCurrentBranch();
            var stocks = _goodReceivedNoteMasterService.GetStocks(branch.BranchCode);
            int id = 1;
            var data = new DataSourceResult()
            {
                Data = stocks.Select(x =>
                {
                    var m = new Stocks();

                    m.Id = id;
                    m.SubItemName = x.SubItemName;
                    m.Qty = x.Qty;
                    m.ItemCode = x.ItemCode;
                    id++;
                    return m;
                }),
                Total = stocks.TotalCount
            };

            return Json(data);
        }
    }
}
