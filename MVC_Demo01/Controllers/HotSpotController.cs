using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Text;

namespace Job_Demo.Controllers
{
    public class HotSpotController : Controller
    {
        // GET: HotSpot
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //非同步
        public async Task<ActionResult> Index() 
        {
            ArrayList aryPeople = new ArrayList();
            string targetURL = "https://data.ntpc.gov.tw/api/datasets/04958686-1B92-4B74-889D-9F34409B272B/json/preview";
            HttpClient client = new HttpClient();
            var webClient = new WebClient { Encoding = Encoding.UTF8 };



            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURL);

            //List<HotSpot> collection = JsonConvert.DeserializeObject<List<HotSpot>>(response);
            //String json = JsonObject.fromObject(response);

           // var collection = JsonConvert.DeserializeObject<IEnumerable<HotSpot>>(response);

            //ViewBag.Result = response;
            //string SerializerData = ((JavaScriptSerializer)new JavaScriptSerializer()).Serialize(response);
            //var SerializerData2 = ((JavaScriptSerializer)new JavaScriptSerializer()).Deserialize<List<Dictionary<string, HotSpot>>>(response);
            //var collection = JsonConvert.DeserializeObject<List<HotSpot>>(response);
            //ViewBag.Result = SerializerData;
            //ViewBag.Result = SerializerData2;
            //ArrayList aryNewPeople = JsonConvert.DeserializeObject<ArrayList>(response);
            //ViewBag.Result = aryNewPeople;
            ViewBag.Result = response;
            //List<HotSpot> hs = collection.Take(10).ToList();
            return View();
        }


        public class HotSpot2
        {


            public string id { get; set; }
            public string spot_name { get; set; }

            public string type { get; set; }

            public string company { get; set; }

            public string district { get; set; }

            public string address { get; set; }

            public string apparatus_name { get; set; }

            public string latitude { get; set; }

            public string longitude { get; set; }

            public string twd97X { get; set; }

            public string twd97Y { get; set; }

            public string wgs84aX { get; set; }

            public string wgs84aY { get; set; }


        }
    }
}