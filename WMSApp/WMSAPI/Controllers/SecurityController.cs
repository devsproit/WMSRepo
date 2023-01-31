using Microsoft.AspNetCore.Mvc;

namespace WMSAPI.Controllers
{
    public class SecurityController : BaseAdminController
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }
        [NonAction]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
