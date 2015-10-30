﻿using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.DAL;
using CodeFirst;
using static App.Validation.EmployeeValidator;

namespace App.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDataAccessObject dataAccessObject = new EmployeeDataAccessObject();

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (BeValueUnique(employee))
            {
                dataAccessObject.Add(employee);
                return RedirectToAction("Index", "Home");
            }

            return View("Apologize");
        }

        [HttpGet]
        public ActionResult ShowEmployees()
        {
            ICollection<Employee> toTransfer = dataAccessObject.GetAll();
            return View(toTransfer);
        }

        [HttpPost]
        public ActionResult RemoveEmployee(int id)
        {
            Employee employee = dataAccessObject.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult RemoveConfirmed(int id)
        {
            dataAccessObject.Remove(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditEmployee(int id)
        {
            Employee employee = dataAccessObject.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditConfirmed(Employee employee)
        {
            dataAccessObject.Edit(employee);
            return RedirectToAction("Index", "Home");          
        }
    }
}