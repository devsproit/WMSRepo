using Application.Services;
using AutoMapper;
using ClosedXML.Excel;
using Domain.Model;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMSWebApp.ViewModels;
using static System.Net.WebRequestMethods;

namespace WMSWebApp.Controllers
{
    public class IntrasitController : Controller
    {
        private readonly IIntrasitHelper _IntrasitHelper;
        private readonly IMapper _mapper;
        private readonly IWorkContext _workContext;
        public IntrasitController(IIntrasitHelper intrasitHelper, IMapper mapper, IWorkContext workContext)
        {
            _IntrasitHelper = intrasitHelper;
            _mapper = mapper;
            _workContext = workContext;
        }

        public IActionResult Index()
        {
            List<Intrasitc> intrasitcs = new List<Intrasitc>();
            try
            {
                var data = _IntrasitHelper.GetAllIntrasit();
                for (int i = 0; i < data.Count; i++)
                {
                    Intrasitc objDate = new Intrasitc();
                    objDate.Id = data[i].Id;
                    objDate.PurchaseOrder = data[i].PurchaseOrder;
                    objDate.Branch = data[i].Sender_Branch;
                    objDate.ItemCode = data[i].Item_Code;
                    objDate.SubItemCode = data[i].SubItem_Code;
                    objDate.MaterialDescription = data[i].Material_Description;
                    objDate.Qty = data[i].Qty;
                    objDate.Unit = data[i].Unit;
                    objDate.Amt = data[i].Amt;
                    intrasitcs.Add(objDate);
                }
            }
            catch (Exception ex)
            {

            }
            return View(intrasitcs);

        }

        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            IntransitViewModel intransitViewModel = new IntransitViewModel();
            List<Intrasitc> intrasitcs = new List<Intrasitc>();
            intransitViewModel.intrasitcList = intrasitcs;
            intransitViewModel.intrasitc = new Intrasitc();
            try
            {
                //var data = _IntrasitHelper.GetAllIntrasit();
                //for (int i = 0; i < data.Count; i++)
                //{
                //    Intrasitc objDate = new Intrasitc();
                //    objDate.Id = data[i].Id;
                //    objDate.Amt = data[i].Amt;
                //    objDate.Qty = data[i].Qty;
                //    objDate.ETA = data[i].ETA;
                //    objDate.MaterialDescription = data[i].MaterialDescription;
                //    objDate.Branch = data[i].Branch;
                //    objDate.ItemCode = data[i].Item_Code;
                //    objDate.SubItemName = data[i].SubItem_Name;
                //    objDate.SubItemCode = data[i].SubItem_Code;
                //    objDate.PurchaseOrder = data[i].PurchaseOrder;
                //    intrasitcs.Add(objDate);
                //}
                var listBranch = _IntrasitHelper.GetAllBranches();
                var listCompany = _IntrasitHelper.GetAllCompany();
                var listItem = _IntrasitHelper.GetAllItem();
                listCompany.Insert(0, new CompanyDb { Id = 0, CompanyName = "Select" });
                listBranch.Insert(0, new Branch { Id = 0, BranchName = "Select" });
                listItem.Insert(0, new ItemDb { Id = 0, ItemName = "Select" });
                ViewBag.ListofCompany = listCompany;
                ViewBag.listBranch = listBranch;
                ViewBag.listItem = listItem;
                //intransitViewModel.listSenderBranch = listBranch.ToList();
                //intransitViewModel.listSenderCompany = listCompany.ToList();
                // intrasitcs = _mapper.Map<List<Intrasitc>>(data);
            }
            catch (Exception ex)
            {
                throw;
            }
            return PartialView(intransitViewModel);
            //return View();
        }

        // POST: CompaniesController/Create
        [HttpPost]
        public JsonResult Create([FromBody] IntransitViewModel intransitViewModel)
        {
            try
            {
                // var intrasit = _mapper.Map<IntrasitDb>(intrasitc);
                var branch = _workContext.GetCurrentBranch().Result;
                foreach (var item in intransitViewModel.intrasitcList)
                {
                    IntrasitDb intrasitDb = new IntrasitDb();
                    intrasitDb.Sender_Branch = null;
                    intrasitDb.PurchaseOrder = item.PurchaseOrder;
                    intrasitDb.Sender_Company = item.SenderCompany;
                    intrasitDb.SubItem_Name = null;
                    intrasitDb.SubItem_Code = item.SubItemCode;
                    intrasitDb.Material_Description = item.MaterialDescription;
                    intrasitDb.Unit = item.Unit;
                    intrasitDb.Amt = item.Amt;
                    intrasitDb.Qty = item.Qty;
                    intrasitDb.Item_Code = item.ItemCode;
                    intrasitDb.Login_Branch = branch.BranchName;
                    _IntrasitHelper.CreateNewIntrasit(intrasitDb);
                }
                return Json(new { success = true, message = "Saved Successfully" });
                // return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Not Saved Successfully" });
                //return View(intrasitc);
            }
        }

        [HttpPost]
        public IActionResult OnPostMyUploader(IFormFile importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {

                //Getting FileName
                var fileName = Path.GetFileName(importFile.FileName);
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                DataSet ds = new DataSet();
                using (var target = new MemoryStream())
                {
                    importFile.CopyTo(target);
                    using (var reader = ExcelReaderFactory.CreateReader(target))
                    {
                        ds = reader.AsDataSet();
                    }
                    //var fileData = GetDataFromCSVFile(ds);
                    GetDataFromCSVFile(ds);
                }


                //var dtEmployee = fileData.ToDataTable();
                //var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured)
                //{
                //    TypeName = "dbo.tblTypeEmployee",
                //    Value = dtEmployee
                //};
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }
        private void GetDataFromCSVFile(DataSet ds)
        {

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("LoginBranch", typeof(string));
                dt.Columns.Add("SenderCompany", typeof(string));
                dt.Columns.Add("Branch", typeof(string));
                dt.Columns.Add("PurchaseOrder", typeof(string));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("SubItemCode", typeof(string));
                dt.Columns.Add("SubItemName", typeof(string));
                dt.Columns.Add("MaterialDescription", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("Amt", typeof(string));
                DataTable dt2 = ds.Tables["Sheet1"];

                foreach (DataRow dr in dt2.Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
                dt.Rows[0].Delete();
                dt.AcceptChanges(); dt2.AcceptChanges();
                dt.Columns["LoginBranch"].ColumnName = "LoginBranch";
                dt.Columns["SenderCompany"].ColumnName = "SenderCompany";
                dt.Columns["Branch"].ColumnName = "Branch";
                dt.Columns["PurchaseOrder"].ColumnName = "PurchaseOrder";
                dt.Columns["ItemCode"].ColumnName = "ItemCode";
                dt.Columns["SubItemCode"].ColumnName = "SubItemCode";
                dt.Columns["SubItemName"].ColumnName = "SubItemName";
                dt.Columns["MaterialDescription"].ColumnName = "MaterialDescription";
                dt.Columns["Qty"].ColumnName = "Qty";
                dt.Columns["Unit"].ColumnName = "Unit";
                dt.Columns["Amt"].ColumnName = "Amt";
                _IntrasitHelper.blukUpload(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetSubItem(int subItemId)
        {
            var data = _IntrasitHelper.GetSubItem(subItemId);
            return Json(data);
        }//GetMaterialDesc

        [HttpGet]
        public JsonResult GetMaterialDesc(int subItemId)
        {
            var data = _IntrasitHelper.GetSubItem(subItemId);
            return Json(data);
        }


        public IActionResult DownloadFile()
        {
            //var memory = DownloadSingleFile("IntransitTemplate.xlsx", "wwwroot\\template");
            //return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntransitTemplate.xlsx");
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "IntransitTemplate.xlsx";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet =
                workbook.Worksheets.Add("Sheet1");
                worksheet.Cell(1, 1).Value = "LoginBranchId";
                worksheet.Cell(1, 2).Value = "SenderCompanyId";
                worksheet.Cell(1, 3).Value = "Branch";
                worksheet.Cell(1, 4).Value = "PurchaseOrder";
                worksheet.Cell(1, 5).Value = "ItemCode";
                worksheet.Cell(1, 6).Value = "SubItemCode";
                worksheet.Cell(1, 7).Value = "SubItemName";
                worksheet.Cell(1, 8).Value = "MaterialDescription";
                worksheet.Cell(1, 9).Value = "Qty";
                worksheet.Cell(1, 10).Value = "Unit";
                worksheet.Cell(1, 11).Value = "Amt";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

        private MemoryStream DownloadSingleFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream();
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }
    }
}
