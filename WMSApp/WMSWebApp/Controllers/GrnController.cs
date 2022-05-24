using Microsoft.AspNetCore.Mvc;

namespace WMSWebApp.Controllers
{
    public class GrnController : Controller
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        #endregion

    }
}
