using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Job_Demo.Models
{


    public class HotSpotModel
    {


        [Display(Name = "熱點代碼")]
        public string id { get; set; }

        [Display(Name = "熱點名稱")]
        public string spot_name { get; set; }

        [Display(Name = "熱點類別")]
        public string type { get; set; }

        [Display(Name = "業者")]
        public string company { get; set; }

        [Display(Name = "鄉鎮市區")]
        public string district { get; set; }

        [Display(Name = "地址")]
        public string address { get; set; }

        [Display(Name = "機關構名稱")]
        public string apparatus_name { get; set; }

        [Display(Name = "緯度")]
        public string latitude { get; set; }

        [Display(Name = "經度")]
        public string longitude { get; set; }

        [Display(Name = "twd97緯度")]
        public string twd97X { get; set; }

        [Display(Name = "twd97經度")]
        public string twd97Y { get; set; }

        [Display(Name = "wgs84a緯度")]
        public string wgs84aX { get; set; }

        [Display(Name = "wgs84a經度")]
        public string wgs84aY { get; set; }

        public List<string> spots = new List<string>();

    }
}