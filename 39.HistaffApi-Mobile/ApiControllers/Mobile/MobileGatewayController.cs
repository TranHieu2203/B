using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HiStaffAPI.ProfileBusiness;
using HiStaffAPI.AppCommon.PortalModel;
using System.Web.Script.Serialization;
using HiStaffAPI.AppConstants;

namespace HiStaffAPI.ApiControllers.Mobile
{
    [RoutePrefix(ApiName.PrefixApiSystem)]
    public class MobileGatewayController : ApiController
    {
        // GET: MobileGateway
      [Route("get-config/{CustomerCode}")]
      [HttpGet]
      public DataConfig GetConfig(string CustomerCode)
      {
        try
        { 
                DataConfig dataConfig = new DataConfig();
                dataConfig.message = "";
                dataConfig.responseStatus = 1;
                Env _env = new Env();
                _env.name = "HSVDEV";
                _env.linkApi = "https://api-hsv-dev.histaff.online/API/MOBILE/";
                _env.linkServerImage = "https://phsv-dev.histaff.online/EmployeeImage/";
                _env.token = "MWM2NjJjZjU3YWYzZTFjZGVhNjY1YWVkNTMzY2ExN2IuMjAyMTA4MDExOTM3MTguMjAyMTA4MzExOTM3MTg=";
                _env.timeTokenExpired="2021 - 08 - 31 19:37:18";
                _env.firebaseKey="AAAAx1H7Hqk:APA91bEg3MWvoR1KLzIsT2XYzL9CWBLfrD5wnzCPXvcOlmY4jCGi4ysQztVVBp84hzcWe43qTzqWBY6twElam2DbZhJRsFdrZoIdggkLAKKmj45jJftSJjKn5hAAsf7zcxPcjQsIoQ3M";
                _env.customerKey = "HSVDEV";
                _env.version = "1.1.0";
                dataConfig.env = _env;
                dataConfig.app = null;
                List<screen> screens = new List<screen>();
                screens.Add(new screen
                {
                   container="Dashboard",
                   //navigation ={ "tab", "stack" },
                   tab="Home",
                   companyCode="CORE"
                });
                return dataConfig;
        }
        catch ( Exception ex)
        {
                return new DataConfig();
        }
      }

    }
    public class DataConfig
    {
        public string message { get; set; }
        public int responseStatus { get; set; }
        public Env env { get; set; }
        public string [] app { get; set; }
        public List<screen> screens { get; set; }
    }
    public class Env
    {
        public string name { get; set; }
        public string linkApi { get; set; }
        public string linkServerImage { get; set; }
        public string token { get; set; }
        public string timeTokenExpired { get; set; }
        public string firebaseKey { get; set; }
        public string customerKey { get; set; }
        public string version { get; set; }
    }
    public class screen
    {
        public string container { get; set; }
        public string [] navigation { get; set; }
        public string tab { get; set; }
        public string companyCode { get; set; }
    }

}
