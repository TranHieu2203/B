using RestSharp;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HiStaffAPI.AppHelpers
{
    public class FaceServices
    {
        private static string FaceServicesUrl = ConfigurationManager.AppSettings.Get("FaceServices");
        public static IRestResponse CallApi(string FaceServicesUrl, object objData, string authoString="X-KEY", string method = "POST")
        { 
            var client = new RestClient(FaceServicesUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            FaceServicesRequest req = (FaceServicesRequest)objData;
            request.AddParameter("application/json", "{\r\n    \"user_id\": " + req.user_id + ",\r\n  \"user_name\": \"abc\",\r\n \"image_base64\":\""+req.image_base64 + "\",\r\n    \"lable_predict\": 1\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var jsonData = response.Content;
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //var faceServicesData = (object)json_serializer.DeserializeObject(jsonData);
            
            // ------------------------------
            return response;
        }
        public static Task< IRestResponse> Api_Face_Predict(FaceServicesRequest request)
        {
            var newRq = new FaceServicesRequest
            {
                user_id = request.user_id,
                user_name = request.user_name,
                image_base64 = request.image_base64 ,
                lable_predict = request.lable_predict  
            };
            string subUrl = "api-face-predict/";
            // Run task
            var taskBackground = Task<IRestResponse>.Factory.StartNew(() => CallApi(FaceServicesUrl+subUrl , newRq));
            // Cần lấy result 
            taskBackground.Wait();
            return taskBackground;
           
        }
    }
    public class FaceServicesRequest
    {
        public decimal user_id { get; set; }
        public string user_name { get; set; }
        public string image_base64 { get; set; }
        public decimal  lable_predict { get; set; }
    }
    public class FaceServicesData
    {
        public decimal user_id { get; set; }
        public string user_name { get; set; }
        public decimal  lable_predict { get; set; }
    }

}