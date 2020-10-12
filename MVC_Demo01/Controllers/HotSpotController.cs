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
using Newtonsoft.Json.Linq;
using System.Data;
using Job_Demo.Models;

namespace Job_Demo.Controllers
{
    public class HotSpotController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ArrayList aryPeople = new ArrayList();
            string targetURL = "https://data.ntpc.gov.tw/api/datasets/04958686-1B92-4B74-889D-9F34409B272B/json/preview";
            HttpClient client = new HttpClient();
            var webClient = new WebClient { Encoding = Encoding.UTF8 };


            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURL);
            List<Root> spots = JsonConvert.DeserializeObject<List<Root>>(response);
            //var collection = JsonConvert.DeserializeObject<IEnumerable<HotSpot>>(json);

            // List<RootObject> datalist = JsonConvert.DeserializeObject<List<RootObject>>(response);
            foreach (var name in spots)
            {
                Response.Write("熱點代碼: " + name.id + "<br>熱點名稱: " + name.spot_name
                    + "<br>熱點類別: " + name.type + "<br>業者: " + name.company
                    + "<br>鄉鎮市區: " + name.district + "<br>地址: " + name.address
                    + "<br>機關構名稱: " + name.apparatus_name + "<br>緯度: " + name.latitude
                    + "<br>經度: " + name.longitude + "<br>twd97緯度: " + name.twd97X + "<br>twd97經度: " + name.twd97Y
                    + "<br>wgs84a緯度: " + name.wgs84aX + "<br>wgs84a經度: " + name.wgs84aY + "<br>");

            }

            //ViewBag.List = response;
            return View();



        }
        public class Root
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

        //public ActionResult Index() {

        //    //JSON字串
        //    string Json = "{ 'aa': [ { 'id': 1, 'item': '這是第1個項目' }, { 'id': 2, 'item': '這是第2個項目' }, { 'id': 3, 'item': '這是第3個項目' }, { 'id': 4, 'item': '這是第4個項目' } ] }";

        //    //將JSON字串轉為DataSet
        //    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(Json);

        //    //建立DataTable，並塞進dataSet的值
        //    DataTable dataTable = dataSet.Tables["aa"];

        //    //顯示DataTable的筆數
        //    Response.Write("共" + dataTable.Rows.Count + "筆資料</br>");

        //    //顯示DataTable的筆數
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        Response.Write(row["id"] + " - " + row["item"]);
        //    }
        //    return View();

        //}
        //public ActionResult Index()
        //{
        //    string json = @"{ 'Name':'C#','Age':'3000','ID':'1','Sex':'女'}";
        //    //var collection = JsonConvert.DeserializeObject<IEnumerable<HotSpot>>(json);
        //    Student descJsonStu = JsonConvert.DeserializeObject<Student>(json);
        //    ViewBag.List = descJsonStu;
        //    Response.Write(string.Format("反序列化： ID={0},Name={1},Sex={2},Sex={3}", descJsonStu.ID, descJsonStu.Name, descJsonStu.Age, descJsonStu.Sex));
        //    return View();
        //}

        //public class Student
        //{
        //    public int ID { get; set; }

        //    public string Name { get; set; }

        //    public int Age { get; set; }

        //    public string Sex { get; set; }
        //}

    }
}