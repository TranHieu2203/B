using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HiStaffAPI.AppHelpers
{
    public class FirebaseHelper
    {

        private static string FireBaseLink = ConfigurationManager.AppSettings.Get("FilebaseApiNotify");
        private static string FireBaseKey = ConfigurationManager.AppSettings.Get("FilebaseApiKey");


        public static string CallApi(string apiLink, object objData, string authoString, string method = "POST")
        {
            try
            {
                // ------------------------------
                string linkAPi = apiLink;
                WebRequest rq = WebRequest.Create(linkAPi);
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(objData);
                var bodyByte = Encoding.UTF8.GetBytes(body);
                string rsString = "";

                // request 
                rq.ContentType = "application/json";
                rq.Method = method;
                rq.ContentLength = bodyByte.Length;
                if (!string.IsNullOrEmpty(authoString))
                    rq.Headers.Add("Authorization", authoString);

                // send request
                using (Stream resStream = rq.GetRequestStream())
                {
                    resStream.Write(bodyByte, 0, bodyByte.Length);
                }

                // get response
                var response = rq.GetResponse();
                using (var rs = new StreamReader(response.GetResponseStream()))
                {
                    rsString = rs.ReadToEnd();
                }

                LogHelper.WriteGeneralLog("CallAPI: " + rsString);
                // result -> convert object
                return rsString;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// Gửi notify theo nội dung các tham số truyền vào (cấu hình link firebase, key theo web.config)
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strBody"></param>
        /// <param name="strSubTitle"></param>
        /// <param name="lstAppID"></param>
        /// <remarks></remarks>
        public static void SendNotify(string strTitle, string strBody, string strSubTitle, string[] lstAppID, string topic = "TVCOM")
        {
            try
            {
                var content = new FireBaseRequest.NotifyContent();
                content.sound = "default";
                content.body = strBody;
                content.title = strTitle;
                content.content_available = true;
                content.priority = "high";
                content.icon = "https://image.flaticon.com/icons/png/512/194/194938.png";
                var newRq = new FireBaseRequest
                {
                    registration_ids = lstAppID,
                    notification = content,
                    badge = 1,
                    subtitle = strSubTitle,
                    topic = topic
                };

                // Run task
                var taskBackground = Task<string>.Factory.StartNew(() => CallApi(FireBaseLink, newRq, FireBaseKey));
            }
            // Cần lấy result 
            // taskBackground.Wait()
            // Dim rs = taskBackground.Result
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("SendNotify: " + ex.Message);
            }
        }
    }



    /// <summary>
    /// Object request firebase
    /// </summary>
    /// <remarks></remarks>

    public partial class FireBaseRequest
    {

        /// <summary>
        /// id send
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string[] registration_ids { get; set; }
        public NotifyContent notification { get; set; }
        public NotifyData data { get; set; }
        public int badge { get; set; }
        public string subtitle { get; set; }
        public string topic { get; set; }

        public partial class NotifyContent
        {
            public string sound { get; set; }
            public string body { get; set; }
            public string title { get; set; }
            public bool content_available { get; set; }
            public string priority { get; set; }
            public string icon { get; set; }
        }

        public partial class NotifyData
        {
            public string navigation { get; set; }

            public NotifyData()
            {
                navigation = "registerLeave";
            }
        }
    }

    /// <summary>
    /// Object get response from firebase
    /// </summary>
    /// <remarks></remarks>
    public partial class FireBaseResponse
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public long canonical_ids { get; set; }
        public ResultData[] results { get; set; }

        public partial class ResultData
        {
            public string message_id { get; set; }
            public string error { get; set; }
        }
    }
}
