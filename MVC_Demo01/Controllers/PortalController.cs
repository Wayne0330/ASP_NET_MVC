using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Job_Demo.Models;
using Job_Demo.Services;
using Job_Demo.ViewModel;

namespace Job_Demo.Controllers
{
    public class PortalController : Controller
    {
        private MailService mailService = new MailService();
        private PortalDBService portalDBService = new PortalDBService();
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "HotSpot");
            }
            return View();
        }


        #region 登入
        [HttpPost]
        public ActionResult Login(MemberLoginView LoginMember)
        {
            string ValidateStr =
                portalDBService.AccountLogin(LoginMember.Email, LoginMember.Password);

            if (String.IsNullOrEmpty(ValidateStr))
            {
                string RoleData = portalDBService.GetRole(LoginMember.Email);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1
                    , LoginMember.Email
                    , DateTime.Now                           //開始時間
                    , DateTime.Now.AddMinutes(30)            //三十分到期
                    , false                                  //是否cookie存取
                    , RoleData                               //使用者資料
                    , FormsAuthentication.FormsCookiePath);  //設定儲存路徑
                                                             //資料加密
                string enTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                return RedirectToAction("Index", "HotSpot");
            }
            else
            {
                ModelState.AddModelError("", ValidateStr);
                return View(LoginMember);
            }
        }

        #endregion

        #region 登出
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        #endregion
        [HttpPost]
        public ActionResult Register(MemberRegisterView RegisterMember)
        {
            var isSuccess = true;
            var returnData = new
            {
                // 成功與否
                IsSuccess = isSuccess,
                // ModelState錯誤訊息
                ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            };
            if (ModelState.IsValid)
            {
                //將頁面資料的密碼欄位填入
                RegisterMember.NewMember.Password = RegisterMember.Password;
                //取得信箱驗證碼
                string AuthCode = mailService.GetValidateCode();
                //信箱驗證碼填入
                RegisterMember.NewMember.AuthCode = AuthCode;
                //呼叫service註冊新會員
                portalDBService.Register(RegisterMember.NewMember);
                //取得驗證信範本
                string TempMail = System.IO.File.ReadAllText(
                    Server.MapPath("~/Views/Shared/RegisterEmailTemplate.html"));
                //宣告email驗證用的url
                UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("EmailValidate", "Portal",
                    new { RegisterMember.NewMember.UserName, AuthCode })
                };
                //藉由service將使用者資料填入驗證信
                string MailBody = mailService.GetRegisterMailBody(TempMail
                    , RegisterMember.NewMember.Id.ToString()
                    , RegisterMember.NewMember.UserName
                    , ValidateUrl.ToString().Replace("%3F", "?"));
                mailService.SendRegisterMail(MailBody, RegisterMember.NewMember.Email);
                //存儲註冊信息
                TempData["RegisterState"] = "註冊成功，請去收信驗證Email";
                return RedirectToAction("RegisterResult");

            }

            RegisterMember.Password = null;
            RegisterMember.PasswordCheck = null;
            return View(RegisterMember);
        }

        public ActionResult RegisterResult()
        {
            return View();
        }


        //確認帳號是否註冊過
        public JsonResult AccountCheck(MemberRegisterView RegisterMember)
        {
            return Json(portalDBService.EmailCheck(RegisterMember.NewMember.Email),
                JsonRequestBehavior.AllowGet);
        }

        //接收驗證信連結結果
        public ActionResult EmailValidate(string Id, string Username, string AuthCode)
        {
            ViewData["EmailValidate"] = portalDBService.EmailValidate(Id, Username, AuthCode);
            return View();
        }





        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordView data)
        {
            var cookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            var tickets = FormsAuthentication.Decrypt(cookies.Value);
            string role = tickets.UserData;
            TempData["UpdateDataResult"] = portalDBService.ChangePassword(role, data.Password, data.NewPassword);
            return RedirectToAction("UpdateMemberDataResult","MyAccount");
        }
    }
}