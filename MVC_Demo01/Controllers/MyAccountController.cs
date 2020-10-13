using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job_Demo.Models;
using Job_Demo.ViewModel;
using Job_Demo.Services;
using System.Web.Security;

namespace Job_Demo.Controllers
{
    public class MyAccountController : Controller
    {
        public Job_DemoDBEntities db = new Job_DemoDBEntities();
        public EditMemberDataService editMemberDataService = new EditMemberDataService();
        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditMemberData()
        {

            var cookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            var tickets = FormsAuthentication.Decrypt(cookies.Value);
            string role = tickets.UserData;
            var result = (from x in db.Account where x.Email == role select x).ToList();
            Account Data = editMemberDataService.GetDataByEmail(role);

            return View(Data);

        }

        [HttpPost]
        public ActionResult EditMemberData(Account MemberData)
        {
            var cookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            var tickets = FormsAuthentication.Decrypt(cookies.Value);
            string role = tickets.UserData;
            if (editMemberDataService.CheckUpdate(role))
            {
                editMemberDataService.UpdateAccount(MemberData);
                TempData["UpdateDataResult"] = "修改成功";
                return RedirectToAction("UpdateMemberDataResult");
            }
            return RedirectToAction("Index", "HotSpot");
        }

        public ActionResult UpdateMemberDataResult()
        {
            return View();
        }
    }
}