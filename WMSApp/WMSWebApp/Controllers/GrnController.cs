using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Application.Services.GRN;
using WMS.Data;
namespace WMSWebApp.Controllers
{
    [Authorize]
    public class GrnController : BaseAdminController
    {
        #region Fields
        private readonly IIntrasitService _intrasitService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public GrnController(IIntrasitService intrasitService, IWorkContext workContext)
        {
            _intrasitService = intrasitService;
            _workContext = workContext;
        }
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

            //var intrasitData=_intrasitService.GetPendingPO()
            return View();
        }
        #endregion

    }
}
