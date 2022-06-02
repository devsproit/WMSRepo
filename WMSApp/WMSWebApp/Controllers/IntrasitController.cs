using Application.Services;
using AutoMapper;
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
using WMSWebApp.ViewModels;
using static System.Net.WebRequestMethods;

namespace WMSWebApp.Controllers
{
    public class IntrasitController : Controller
    {
        private readonly IIntrasitHelper _IntrasitHelper;
        private readonly IMapper _mapper;

        public IntrasitController(IIntrasitHelper intrasitHelper, IMapper mapper)
        {
            _IntrasitHelper = intrasitHelper;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
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
                var data = _IntrasitHelper.GetAllIntrasit();
                for (int i = 0; i < data.Count; i++)
                {
                    Intrasitc objDate = new Intrasitc();
                    objDate.Id = data[i].Id;
                    objDate.Amt = data[i].Amt;
                    objDate.Qty = data[i].Qty;
                    objDate.ETA = data[i].ETA;
                    objDate.MaterialDescription = data[i].Material_Description;
                    objDate.Branch = data[i].Sender_Branch;
                    objDate.ItemCode = data[i].Item_Code;
                    objDate.SubItemName = data[i].SubItem_Name;
                    objDate.SubItemCode = data[i].SubItem_Code;
                    objDate.PurchaseOrder = data[i].PurchaseOrder;
                    intrasitcs.Add(objDate);
                }
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Intrasitc intrasitc, IFormCollection collection)
        {
            try
            {
                // var intrasit = _mapper.Map<IntrasitDb>(intrasitc);

                IntrasitDb intrasitDb = new IntrasitDb();
                intrasitDb.Sender_Branch = intrasitc.Branch;
                intrasitDb.PurchaseOrder = intrasitc.PurchaseOrder;
                intrasitDb.Sender_Company = intrasitc.SenderCompany;
                intrasitDb.SubItem_Name = intrasitc.SubItemName;
                intrasitDb.SubItem_Code = intrasitc.SubItemCode;
                intrasitDb.Material_Description = intrasitc.MaterialDescription;
                intrasitDb.Unit = intrasitc.Unit;
                intrasitDb.Amt = intrasitc.Amt;
                intrasitDb.Qty = intrasitc.Qty;
                intrasitDb.Item_Code = intrasitc.ItemCode;
                intrasitDb.Login_Branch = intrasitc.LoginBranch;
                var result = _IntrasitHelper.CreateNewIntrasit(intrasitDb);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(intrasitc);
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
    }
}

