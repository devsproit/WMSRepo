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
using Application.Services.StockMgnt;
using Domain.Model.StockManagement;
using Application.Services.Master;
using Application.Services;
namespace WMSWebApp.Controllers
{
    [Authorize]
    public class GrnController : BaseAdminController
    {
        #region Fields
        private readonly IIntrasitService _intrasitService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IWarehouseService _warehouseService;
        private readonly IGoodReceivedNoteMasterService _goodReceivedNoteMasterService;
        private readonly IItemStockService _itemStockService;
        private readonly IVenderVehicleService _venderVehicleService;
        private readonly IBranchHelper _branchService;

        #endregion

        #region Ctor
        public GrnController(IIntrasitService intrasitService, IWorkContext workContext, IMapper mapper, IWarehouseService warehouseService, IGoodReceivedNoteMasterService goodReceivedNoteMasterService, IItemStockService itemStockService, IVenderVehicleService venderVehicleService, IBranchHelper branchService)
        {
            _intrasitService = intrasitService;
            _workContext = workContext;
            _mapper = mapper;
            _warehouseService = warehouseService;
            _goodReceivedNoteMasterService = goodReceivedNoteMasterService;
            _itemStockService = itemStockService;
            _venderVehicleService = venderVehicleService;
            _branchService = branchService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            CreateModel model = new CreateModel();
            model.Vehicles = _venderVehicleService.VendorVehicles().ToList();

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
            var intrasitData = _intrasitService.GetPendingPO(branch.BranchCode, "0", 0, int.MaxValue).ToList().GetUniqePo();
            return Json(intrasitData);

        }

        [HttpPost]
        public virtual async Task<IActionResult> PODetails(string pono)
        {
            var branch = await _workContext.GetCurrentBranch();
            var intrasitData = _intrasitService.GetPendingPO(branch.BranchCode, pono, 0, int.MaxValue);
            int id = 1;
            var data = new DataSourceResult()
            {
                Data = intrasitData.Select(x =>
                {
                    var m = new Intrasitc();

                    m.Id = x.Id;
                    m.LoginBranch = x.Login_Branch;
                    m.SenderCompany = x.Sender_Company;
                    m.Branch = x.Sender_Branch;
                    m.PurchaseOrder = x.PurchaseOrder;
                    m.ItemCode = x.Item_Code;
                    m.SubItemCode = x.SubItem_Code;
                    m.SubItemName = x.SubItem_Name;
                    m.MaterialDescription = x.Material_Description;
                    m.Qty = x.Qty;
                    m.Unit = x.Unit;
                    m.Amt = x.Amt;
                    m.ETA = x.ETA;
                    m.Sno = id;
                    id++;
                    return m;
                }),
                Total = intrasitData.TotalCount
            };

            return Json(data);
        }

        [HttpGet]
        public virtual IActionResult GetItemDetails(int id)
        {
            var x = _intrasitService.GetById(id);
            var m = new Intrasitc();
            m.Id = x.Id;
            m.LoginBranch = x.Login_Branch;
            m.SenderCompany = x.Sender_Company;
            m.Branch = x.Sender_Branch;
            m.PurchaseOrder = x.PurchaseOrder;
            m.ItemCode = x.Item_Code;
            m.SubItemCode = x.SubItem_Code;
            m.SubItemName = x.SubItem_Name;
            m.MaterialDescription = x.Material_Description;
            m.Qty = x.Qty;
            m.Unit = x.Unit;
            m.Amt = x.Amt;
            m.ETA = x.ETA;
            return Json(m);
        }


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
            var intranicRow = _intrasitService.GetById(model.FirstOrDefault().ItemId);
            var master = new GoodReceivedNoteMaster();
            master.PONo = model.FirstOrDefault().Ponumber;
            master.InvoiceDate = DateTime.Now;
            master.BranchCode = branch.BranchCode;
            master.InvoiceNo = model.FirstOrDefault().invoice;
            master.SenderCompany = intranicRow.Sender_Company;
            foreach (var item in model)
            {
                intranicRow = _intrasitService.GetById(item.ItemId);
                var details = new GoodReceivedNoteDetails()
                {

                    Amount = intranicRow.Amt,
                    AreaId = item.Warea,
                    GoodReceivedNoteMaster = master,
                    ItemCode = intranicRow.Item_Code,
                    MaterialDescription = intranicRow.Material_Description,
                    Qty = Convert.ToInt32(intranicRow.Qty),
                    SubItemCode = intranicRow.SubItem_Code,
                    SubItemName = intranicRow.SubItem_Name,
                    Unit = intranicRow.Unit

                };
                master.GoodReceivedNoteDetails.Add(details);
            }
            _goodReceivedNoteMasterService.Insert(master);
            // update stocks
            foreach (var item in model)
            {
                intranicRow = _intrasitService.GetById(item.ItemId);
                int areaId = master.GoodReceivedNoteDetails.FirstOrDefault(x => x.SubItemCode == intranicRow.SubItem_Code).AreaId;
                AddUpdateStock(intranicRow.SubItem_Code, item.WarehouseId, Convert.ToInt32(intranicRow.Qty), areaId);

            }
            foreach (var item in model)
            {
                var po = _intrasitService.GetById(item.ItemId);
                po.IsGrn = true;
                _intrasitService.Update(po);


            }

            return Json("OK");
        }



        public virtual IActionResult List()
        {
            var branch = _branchService.GetAllBranches();
            return View(branch.ToList());
        }


        [HttpPost]
        public virtual async Task<IActionResult> List(DataSourceRequest request, string branchCode)
        {
            var branch = await _workContext.GetCurrentBranch();
            var grns = _goodReceivedNoteMasterService.GetAllMaster(branchCode, request.Page - 1, request.PageSize);
            var gridData = new DataSourceResult()
            {
                Data = grns.Select(x =>
                {
                    GRNListReportModel m = new GRNListReportModel();
                    m.PONo = x.PONo;
                    m.Id = x.Id;
                    m.InvoiceNo = x.InvoiceNo;
                    m.InvoiceDate = x.InvoiceDate;
                    m.BranchCode = branchCode;
                    var branch = _branchService.GetBranchByCode(branchCode);
                    if (branch != null)
                        m.Branch = branch.BranchName;
                    m.SenderCompany = x.SenderCompany;
                    return m;
                }),
                Total = grns.TotalCount
            };
            return Json(gridData);
        }
        #endregion

        #region Utilities
        protected void AddUpdateStock(string itemCode, int warehouseId, int qty, int areaId)
        {
            //var stock = _itemStockService.ItemByCode(itemCode, warehouseId);
            //if (stock != null)
            //{
            //    var item = _itemStockService.GetById(stock.Id);
            //    item.Qty = item.Qty + qty;
            //    item.LastUpdate = DateTime.Now;
            //    _itemStockService.Update(item);
            //}
            //else
            //{
            //    var item = new InventoryStock();
            //    item.WarehouseId = warehouseId;
            //    item.ItemCode = itemCode;
            //    item.LastUpdate = DateTime.Now;
            //    item.Qty = qty;
            //    _itemStockService.Insert(item);
            //}
            var item = new InventoryStock();
            item.WarehouseId = warehouseId;
            item.ItemCode = itemCode;
            item.LastUpdate = DateTime.Now;
            item.Qty = qty;
            item.AreaId = areaId;
            _itemStockService.Insert(item);
        }
        #endregion

    }
}
