using Job_Demo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Demo.ViewModel
{
    public class MemberRegisterView
    {
        public Account NewMember { get; set; }



        [DisplayName("密碼111111")]
       // [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }


        [DisplayName("確認密碼")]
        //[Compare("Password", ErrorMessage = "兩次密碼不一致")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        public string PasswordCheck { get; set; }



    }
}