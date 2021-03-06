using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WMSWebApp.ViewModels;


namespace WMSWebApp.Controllers
{
    [Authorize]
    public class CustomerController : BaseAdminController
    {
        private readonly ICustomerHelper _CustomerHelper;
        private readonly IMapper _mapper;
        private readonly IConfiguration Configuration;


        public CustomerController(ICustomerHelper CustomerHelper, IMapper mapper, IConfiguration _configuration)
        {
            _CustomerHelper = CustomerHelper;
            _mapper = mapper;
            Configuration = _configuration;
        }


        // GET: CustomerController
        public ActionResult Index()
        {
            List<Customer> Customer = new List<Customer>();
            try
            {
                var data = _CustomerHelper.GetAllCustomer();
                Customer = _mapper.Map<List<Customer>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Customer);
        }
        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var B = new Customer()
            {
                CustomerCategory = "Test",
                CustomerName = "Test",
                CustomerCode = "Test",
                Location = "Test",
                
            };
            return View(B);
        }
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            Root districtslist = new Root();
            HttpClient client = new HttpClient();
            string apiUrl = Configuration.GetValue<string>("districtUrl");
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                districtslist = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
            }
            ViewBag.districts = districtslist.districts;
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer c, IFormCollection collection)
        {
            try
            {
                var Customer = _mapper.Map<CustomerDb>(c);
                var b = _CustomerHelper.CreateNewCustomer(Customer);
                SuccessNotification("New Customer added successfully.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var c = new Customer();
            try
            {
                var data = _CustomerHelper.GetCustomerById(id);
                c = _mapper.Map<Customer>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer c, IFormCollection collection)
        {
            try
            {
                var Customer = _mapper.Map<CustomerDb>(c);
                var b = _CustomerHelper.UpdateCustomerById(Customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

