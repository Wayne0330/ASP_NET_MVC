using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Demo.ViewModel
{
    public class EditMemberDataView
    {

        [DisplayName("會員編號")]
        public string Id { get; set; }


        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string UserName { get; set; }


        [DisplayName("電子郵件")]
        public string Email { get; private set; }


        [DisplayName("生日")]

        public string Birthday { get; set; }

        [DisplayName("手機號碼")]
        public string PhoneNember { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

    }
}