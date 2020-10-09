using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job_Demo.Models;
using Job_Demo.Services;

namespace Job_Demo.Controllers
{
    public class PortalController : Controller
    {
        ProtalDBService data = new ProtalDBService();
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string UserName, string Identity, string Password, string Email) 
        {
            data.DBCreate(UserName, Identity, Password, Email);
            return RedirectToAction("Success");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Password, string Email)
        {
            data.AccountLogin(Password, Email);
            return RedirectToAction("Index");
        }

    }
}