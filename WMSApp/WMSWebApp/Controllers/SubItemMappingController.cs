using Application.Services.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Web.Framework.Infrastructure.Extentsion;
using WMSWebApp.ViewModels.SubItemMapping;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class SubItemMappingController : BaseAdminController
    {
        #region Fields
        private readonly ISubItemWarehouseMappingService _subItemWarehouseMappingService;
        #endregion

        #region Ctor
        public SubItemMappingController(ISubItemWarehouseMappingService subItemWarehouseMappingService)
        {
            _subItemWarehouseMappingService = subItemWarehouseMappingService;
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

        public IActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }

        #endregion

    }
}
