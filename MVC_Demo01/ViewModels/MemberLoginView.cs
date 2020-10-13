using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Demo.ViewModel
{
    public class MemberLoginView
    {
        [DisplayName("信箱")]
        [Required(ErrorMessage = "請輸入信箱")]
        public string Email { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }
}