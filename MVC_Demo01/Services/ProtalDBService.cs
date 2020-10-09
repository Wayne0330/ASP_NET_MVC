using Job_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Job_Demo.Services
{
    public class ProtalDBService
    {
        public Job_Demo.Models.Job_DemoDBEntities db = new Models.Job_DemoDBEntities();

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

    }
}