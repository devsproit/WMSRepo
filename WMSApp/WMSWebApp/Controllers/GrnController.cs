using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WMSWebApp.Controllers
{
    [Authorize]
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

        public virtual IActionResult PODetails(string pono)
        {
            return View();
        }
        #endregion

    }
}
