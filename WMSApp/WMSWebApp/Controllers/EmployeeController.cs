using System;
using System.Collections.Generic;
using Application.Services;
using AutoMapper;
using DatabaseLibrary.SQL;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSWebApp.ViewModels;


namespace WMSWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeHelper _EmployeeHelper;
        private readonly IMapper _mapper;


        public EmployeeController(IEmployeeHelper EmployeeHelper, IMapper mapper)
        {
            _EmployeeHelper = EmployeeHelper;
            _mapper = mapper;
        }


        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> Employee = new List<Employee>();
            try
            {
                var data = _EmployeeHelper.GetAllEmployee();
                Employee = _mapper.Map<List<Employee>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(Employee);
        }
        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var c = new Employee();
            try
            {
                var data = _EmployeeHelper.GetEmployeeById(id);
                c = _mapper.Map<Employee>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee c, IFormCollection collection)
        {
            try
            {
                var Employee = _mapper.Map<EmployeeDb>(c);
                var b = _EmployeeHelper.CreateNewEmployee(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(c);
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var c = new Employee();
            try
            {
                var data = _EmployeeHelper.GetEmployeeById(id);
                c = _mapper.Map<Employee>(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View(c);
        }
        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee c, IFormCollection collection)
        {
            try
            {
                var Employee = _mapper.Map<EmployeeDb>(c);
                var b = _EmployeeHelper.UpdateEmployeeById(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        // POST: EmployeeController/Delete/5
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
