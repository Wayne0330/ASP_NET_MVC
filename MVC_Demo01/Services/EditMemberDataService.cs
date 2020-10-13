using Job_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Demo.Services
{
  
    public class EditMemberDataService
    {
        public Job_DemoDBEntities db = new Job_DemoDBEntities();
        public Account GetDataByEmail(string Email)
        {
            return db.Account.Find(Email);
        }

        public void UpdateAccount(Account UpdateData)
        {
            Account OldData = db.Account.Find(UpdateData.Email);
            OldData.UserName = UpdateData.UserName;
            db.SaveChanges();
        }

        public bool CheckUpdate(string Email)
        {
            Account Data = db.Account.Find(Email);
            return (Data != null);
        }
    }
}