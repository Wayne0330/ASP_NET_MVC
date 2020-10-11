using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Demo.Models
{
    [MetadataType(typeof(MembersMetadata))]
    public partial class Account {
        public class MembersMetadata
        {
            public string Id { get; set; }

            [DisplayName("會員等級")]
            [Required(ErrorMessage = "請輸入會員等級")]
            public string Identity { get; set; }

            [DisplayName("姓名")]
            [StringLength(20, ErrorMessage = "姓名長度最多20字元")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string UserName { get; set; }


            //[DisplayName("密碼")]
            //[Required(ErrorMessage = "請輸入密碼")]
            public string Password { get; set; }


            //[DisplayName("確認密碼")]
            //[Compare("Password", ErrorMessage = "兩次密碼不一致")]
            //[Required(ErrorMessage = "請輸入確認密碼")]
            //public string PasswordCheck { get; set; }


            [DisplayName("Email")]
            [StringLength(200, ErrorMessage = "姓名長度最多200字元")]
            [Required(ErrorMessage = "請輸入Email")]
            [EmailAddress(ErrorMessage = "這不是Email格式")]
            public string Email { get; set; }

            //[DisplayName("驗證碼")]
            //[Required(ErrorMessage = "請輸入驗證碼")]
            public string AuthCode { get; set; }
        }
    }

}