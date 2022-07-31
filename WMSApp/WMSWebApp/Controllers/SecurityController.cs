using Microsoft.AspNetCore.Mvc;

namespace WMSWebApp.Controllers
{
    public class SecurityController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
