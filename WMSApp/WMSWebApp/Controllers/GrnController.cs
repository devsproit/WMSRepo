using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Application.Services.GRN;
using WMS.Data;
using System.Linq;
using System.Threading.Tasks;
using WMSWebApp.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using WMSWebApp.ViewModels.GRN;
namespace WMSWebApp.Controllers
{
    [Authorize]
    public class GrnController : BaseAdminController
    {
        #region Fields
        private readonly IIntrasitService _intrasitService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GrnController(IIntrasitService intrasitService, IWorkContext workContext, IMapper mapper)
        {
            _intrasitService = intrasitService;
            _workContext = workContext;
            _mapper = mapper;
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

        [HttpGet]
        public virtual async Task<IActionResult> PendingPO()
        {
            var branch = await _workContext.GetCurrentBranch();
            var intrasitData = _intrasitService.GetPendingPO(branch.BranchCode, "", 0, int.MaxValue).ToList().GetUniqePo();
            return Json(intrasitData);

        }

        [HttpGet]
        public virtual async Task<IActionResult> PODetails(string pono)
        {
            var branch = await _workContext.GetCurrentBranch();
            var intrasitData = _intrasitService.GetPendingPO(branch.BranchCode, pono, 0, int.MaxValue).ToList();
            List<Intrasitc> intrasit = _mapper.Map<List<Intrasitc>>(intrasitData);
            return Json(intrasit);
        }


        [HttpPost]
        public virtual IActionResult Complete([FromBody] GRNModel model)
        {

            return Json("");
        }
        #endregion

    }
}
