using Job_Demo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.ModelBinding;

namespace Job_Demo.Services
{
    public class ProtalDBService
    {
        public Job_DemoDBEntities db = new Job_DemoDBEntities();

        public List<Account> GetAccount() {
            return (db.Account.ToList());
        }

        public void Register(Account NewMember){
            //Account AccountData = new Account();
            //AccountData.UserName = Username;
            //AccountData.Password = Password;
            //AccountData.Email = Email;
            //AccountData.Identity = Identity;
            //NewMember.Password = HashPassword(NewMember.Password);
            //NewMember.Time = DateTime.Now;
            NewMember.Password = HashPassword(NewMember.Password);
            db.Account.Add(NewMember);
            db.SaveChanges();
        }


        //Hash密碼
        public string HashPassword(string Password) {
            string saltkey = "adfads2e13DCSDF213";
            string saltAndPassword = String.Concat(Password, saltkey);
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashDate = sha1Hasher.ComputeHash(PasswordData);

            string Hashresult = "";
            for (int i = 0; i < HashDate.Length; i++)
            {
                Hashresult += HashDate[i].ToString("x2");
            }

            return Hashresult;
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
        public string EmailValidate(string Id, string UserName, string AuthCode)
        {
            //取得會員資料
            Account ValidateAccount = db.Account.Find(Id);
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