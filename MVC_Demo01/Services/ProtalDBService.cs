using Job_Demo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Job_Demo.Services
{
    public class ProtalDBService
    {
        public Job_DemoDBEntities db = new Job_DemoDBEntities();

        public List<Account> GetAccount() {
            return (db.Account.ToList());
        }

        public void DBCreate(string Username, string Password, string Email, string Identity) {
            Account AccountData = new Account();
            AccountData.UserName = Username;
            AccountData.Password = Password;
            AccountData.Email = Email;
            AccountData.Identity = Identity;
            AccountData.Time = DateTime.Now;
            db.Account.Add(AccountData);
            db.SaveChanges();
        }

        public void AccountLogin(string Password, string Email)
        {
            Account AccountData = new Account();
            AccountData.Password = Password;
            AccountData.Email = Email;
        }

        //確認註冊的信箱是否有註冊過
        public bool EmailCheck(string Email)
        {
            Account serch = db.Account.Find(Email);
            bool result = (serch == null);
            return result;
        }

        //信箱驗證
        #region 信箱驗證
        public string EmailValidate(string UserName, string AuthCode)
        {
            //取得會員資料
            Account ValidateAccount = db.Account.Find(UserName);
            string ValidateStr = string.Empty;
            if (ValidateAccount != null)
            {
                if (ValidateAccount.AuthCode == AuthCode)
                {
                    ValidateAccount.AuthCode = string.Empty;
                    db.SaveChanges();
                    ValidateStr = "信箱驗證成功";
                }
                else 
                {
                    ValidateStr = "驗證碼錯誤，請重新確認在註冊";
                }
            }
            return ValidateStr;
        }
        #endregion
    }
}