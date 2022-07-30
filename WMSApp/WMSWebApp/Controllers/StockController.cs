using Microsoft.AspNetCore.Mvc;
using Domain.Model.StockManagement;
using Application.Services.StockMgnt;
using WMS.Data;
using System.Linq;
using WMS.Web.Framework.Infrastructure.Extentsion;
using Application.Services;
using Application.Services.WarehouseMaster;
namespace WMSWebApp.Controllers
{
    public class StockController : BaseAdminController
    {

        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IItemStockService _itemStockService;
        private readonly IBranchHelper _branchService;
        private readonly IWarehouseService _warehouseService;

        #endregion

        #region Ctor
        public StockController(IWorkContext workContext, IItemStockService itemStockService, IWarehouseService warehouseService)
        {
            _workContext = workContext;
            _itemStockService = itemStockService;
            _warehouseService = warehouseService;
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


        //public IActionResult List(DataSourceRequest request, int branchId, int warehouseId)
        //{
        //    var branch=_branchService.GetBranchById(branchId);
        //    var warehouse=_warehouseService.GetById(warehouseId);
        //    var stocks=_itemStockService.GetItemStocks(branch.BranchCode,)
        //    return json();

        //}
        #endregion

    }
}
