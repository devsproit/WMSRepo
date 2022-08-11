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
using WMS.Web.Framework.Infrastructure.Extentsion;
using Application.Services.WarehouseMaster;
using System;
using Domain.Model.GRN;
using Application.Services.PO;
using WMSWebApp.ViewModels.PO;
using Application.Services.SRN;
using Domain.Model.SRN;

namespace WMSWebApp.Controllers
{
    [Authorize]
    public class SrnController : BaseAdminController
    {
        #region Fields
        // private readonly IIntrasitService _intrasitService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IPurchaseOrder _purchaseOrder;
        private readonly IWarehouseService _warehouseService;
        private readonly ISRNNoteMasterService _srnReceivedNoteMasterService;
        private readonly ISrnPo _srnPo;
        private readonly IIntrasitService _intrasitService;
        #endregion

        #region Ctor
        public SrnController(IWorkContext workContext, IMapper mapper, ISrnPo srnPo, IPurchaseOrder purchaseOrder,
            IWarehouseService warehouseService, ISRNNoteMasterService sRNNoteMasterService, IIntrasitService intrasitService)
        {
            _srnPo = srnPo;
            _workContext = workContext;
            _mapper = mapper;
            _purchaseOrder = purchaseOrder;
            _warehouseService = warehouseService;
            _srnReceivedNoteMasterService = sRNNoteMasterService;
            _intrasitService = intrasitService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateAsync()
        {
            CreateModel model = new CreateModel();

            //var branch = await _workContext.GetCurrentBranch();
            //List<WarehouseModel> warehouseModel = new List<WarehouseModel>();
            //foreach (var item in branch.BranchWiseWarehouses)
            //{
            //    WarehouseModel warehouse = new WarehouseModel();
            //    warehouse.WarehouseName = item.Warehouse.WarehouseName;
            //    warehouse.WarehouseCode = item.Warehouse.WarehouseCode;
            //    warehouse.Id = item.Warehouse.Id;
            //    List<WarehouseZoneAreaModel> ZoneList = new List<WarehouseZoneAreaModel>();
            //    foreach (var zone in item.Warehouse.WarehouseZones)
            //    {
            //        var Zone = new WarehouseZoneAreaModel();
            //        Zone.ZoneName = zone.ZoneName;
            //        Zone.ZoneCode = zone.ZoneCode;
            //        ZoneList.Add(Zone);

            //    }
            //    warehouse.ZoneAreaList = ZoneList;
            //    warehouseModel.Add(warehouse);
            //}
            //model.Warehouse = warehouseModel;

            return View(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> PendingPO()
        {
            var branch = await _workContext.GetCurrentBranch();
            var intrasitData = _purchaseOrder.GetPendingPO(branch.BranchCode, "0", 0, int.MaxValue).ToList().GetUniqePoOrder();
            return Json(intrasitData);

        }

        [HttpPost]
        public virtual async Task<IActionResult> PODetails(string pono)
        {
            var branch = await _workContext.GetCurrentBranch();
            var srnData = _srnPo.GetSrnDetails(branch.BranchCode, pono, 0, int.MaxValue).ToList();
            int id = 1;
            var data = new DataSourceResult()
            {
                Data = srnData.Select(x =>
                {
                    var m = new PoSrnViewModel();
                    m.Id = x.Id;
                    //   m.LoginBranch = x.BranchCode;
                    //m.SenderCompany = x.;
                    //m.Branch = x.Sender_Branch;
                    m.PurchaseOrder = x.PONumber;
                    //  m.ItemCode = x.SubItemCode;
                    m.SubItemCode = x.SubItemCode;
                    m.SubItemName = x.SrnPOItem;
                    m.MaterialDescription = x.SrnPOSrnCause;
                    m.Qty = x.SrnPOQty;
                    m.Unit = "0";
                    m.Amt = 0;
                    // m.ETA = x.ETA;
                    m.Sno = id;
                    id++;
                    return m;
                }),
                Total = srnData.Count()
            };
            return Json(data);
        }

        //[HttpGet]
        //public virtual IActionResult GetItemDetails(int id)
        //{
        //    var x = _intrasitService.GetById(id);
        //    var m = new Intrasitc();
        //    m.Id = x.Id;
        //    m.LoginBranch = x.Login_Branch;
        //    m.SenderCompany = x.Sender_Company;
        //    m.Branch = x.Sender_Branch;
        //    m.PurchaseOrder = x.PurchaseOrder;
        //    m.ItemCode = x.Item_Code;
        //    m.SubItemCode = x.SubItem_Code;
        //    m.SubItemName = x.SubItem_Name;
        //    m.MaterialDescription = x.Material_Description;
        //    m.Qty = x.Qty;
        //    m.Unit = x.Unit;
        //    m.Amt = x.Amt;
        //    m.ETA = x.ETA;
        //    return Json(m);
        //}


        [HttpGet]
        public virtual async Task<IActionResult> Warehouse()
        {
            var branch = await _workContext.GetCurrentBranch();
            List<WarehouseModel> warehouseModel = new List<WarehouseModel>();
            foreach (var item in branch.BranchWiseWarehouses)
            {
                WarehouseModel warehouse = new WarehouseModel();
                warehouse.WarehouseName = item.Warehouse.WarehouseName;
                warehouse.WarehouseCode = item.Warehouse.WarehouseCode;
                warehouse.Id = item.Warehouse.Id;
                List<WarehouseZoneAreaModel> ZoneList = new List<WarehouseZoneAreaModel>();
                foreach (var zone in item.Warehouse.WarehouseZones)
                {
                    var Zone = new WarehouseZoneAreaModel();
                    Zone.ZoneName = zone.ZoneName;
                    Zone.ZoneCode = zone.ZoneCode;
                    ZoneList.Add(Zone);

                }
                warehouse.ZoneAreaList = ZoneList;
                warehouseModel.Add(warehouse);
            }
            return Json(warehouseModel);
        }


        [HttpGet]
        public virtual IActionResult WarehouseZone(int warehouseid)
        {
            var areas = _warehouseService.GetAllZone(warehouseid);
            List<WarehouseZoneAreaModel> model = new List<WarehouseZoneAreaModel>();
            foreach (var item in areas.ToList())
            {
                WarehouseZoneAreaModel zone = new WarehouseZoneAreaModel();
                zone.ZoneName = item.ZoneName;
                zone.ZoneCode = item.ZoneCode;

                zone.Id = item.Id;
                model.Add(zone);
            }
            return Json(model);
        }

        [HttpGet]
        public virtual IActionResult WarehouseArea(int warehouseid, int zoneid)
        {
            var areas = _warehouseService.GetAllWarehouseArea(warehouseid, zoneid, true);
            List<WarehouseArea> model = new List<WarehouseArea>();
            foreach (var item in areas.ToList())
            {
                WarehouseArea area = new WarehouseArea();
                area.AreaName = item.AreaName;
                area.AreaCode = item.AreaCode;
                area.Size = Convert.ToInt32(item.Size);
                area.Id = item.Id;
                model.Add(area);
            }
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult Complete([FromBody] List<CreateModel> model)
        {
            var branch = _workContext.GetCurrentBranch().Result;
            var intranicRow = _srnPo.GetById(model.FirstOrDefault().ItemId);
            var master = new SrnReceivedNoteMaster();
            master.PONo = model.FirstOrDefault().Ponumber;
            master.InvoiceDate = DateTime.Now;
            master.BranchCode = branch.BranchCode;
            master.InvoiceNo = model.FirstOrDefault().invoice;
            // master.SenderCompany = intranicRow.Sender_Company;
            foreach (var item in model)
            {
                // intranicRow = _intrasitService.GetById(item.ItemId);
                var details = new SrnReceivedNoteDetails()
                {

                    Amount = 0,
                    AreaId = item.Warea,
                    SrnReceivedNoteMaster = master,
                    ItemCode = intranicRow.SubItemCode,
                    MaterialDescription = intranicRow.SrnPOSrnCause,
                    Qty = Convert.ToInt32(intranicRow.SrnPOQty),
                    SubItemCode = intranicRow.SubItemCode,
                    SubItemName = intranicRow.SrnPOSubItem,
                    Unit = "0"

                };
                master.SrnReceivedNoteDetails.Add(details);
            }
            _srnReceivedNoteMasterService.Insert(master);
            foreach (var item in model)
            {
                var sRNPo = _srnPo.GetById(item.ItemId);
                sRNPo.IsProcess = true;
                _srnPo.Update(sRNPo);

                var po = _purchaseOrder.GetById(item.ItemId);
                po.ProcessStatus = true;
                _purchaseOrder.Update(po);
            }

            return Json("OK");
        }



        public virtual IActionResult List()
        {
            return View();
        }


        [HttpPost]
        public virtual IActionResult List(DataSourceRequest request)
        {
            return View();
        }
        #endregion

    }
}
