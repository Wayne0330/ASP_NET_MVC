using System;
using System.Linq;
using System.Web.Mvc;
using Job_Demo.Services;
using Job_Demo.ViewModel;

namespace Job_Demo.Controllers
{
    public class PortalController : Controller
    {
        private MailService mailService = new MailService();
        private ProtalDBService protalDBService = new ProtalDBService();
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Protal");

            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberRegisterView RegisterMember)
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
                protalDBService.Register(RegisterMember.NewMember);
                return RedirectToAction("Success");
            }
            else
            {
                isSuccess = false;
                returnData = new
                {
                    // 成功與否
                    IsSuccess = isSuccess,
                    // ModelState錯誤訊息
                    ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                //return View(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");

                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
            }

            //AccountRegisterView data2 = new AccountRegisterView();
            //data.DBCreate(UserName, Identity, Password, Email);

        }
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Protal");

            return View();
        }

        //[HttpPost]
        //public ActionResult Register(MemberRegisterView memberRegister)
        //{

        //    protalDBService.Register(memberRegister.NewMember);
        //    return View(memberRegister);
        //}
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(string Password, string Email)
        //{
        //    data.AccountLogin(Password, Email);
        //    return RedirectToAction("Index");
        //}


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
                protalDBService.Register(RegisterMember.NewMember);
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
            else {

                isSuccess = false;
                returnData = new
                {
                    // 成功與否
                    IsSuccess = isSuccess,
                    // ModelState錯誤訊息
                    ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
                //return View(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");

                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(returnData), "application/json");
            }
            //RegisterMember.Password = null;
            //RegisterMember.PasswordCheck = null;
            //return View(RegisterMember);
        }

        public ActionResult RegisterResult()
        {
            return View();
        }


        //確認帳號是否註冊過
        public JsonResult AccountCheck(MemberRegisterView RegisterMember)
        {
            return Json(protalDBService.EmailCheck(RegisterMember.NewMember.Email),
                JsonRequestBehavior.AllowGet);
        }

        //接收驗證信連結結果
        public ActionResult EmailValidate(string Id, string Username, string AuthCode)
        {
            ViewData["EmailValidate"] = protalDBService.EmailValidate(Id, Username, AuthCode);
            return View();
        }

    }
}