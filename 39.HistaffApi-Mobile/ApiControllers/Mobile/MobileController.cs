using HiStaffAPI.AppCommon;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.Config;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.MobileModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Attributes;
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

namespace HiStaffAPI.ApiControllers.Mobile
{
    /// <summary>
    /// Api xử lý cho mobile
    /// </summary>
    [RoutePrefix(ApiName.PrefixApi)]
    public partial class MobileController : ApiController
    {
        private readonly IAuthen _authen;
        //private readonly string ConnectionString = ConfigurationManager.AppSettings["DbConnection"];
        private readonly IOracleDBManager _dBTool;
        public MobileController(IAuthen authen, IOracleDBManager DBTool)
        {
            _authen = authen;
            _dBTool = DBTool;
        }

        /// <summary>
        /// Api cho mobile
        /// </summary>
        /// <remarks>
        /// functionName = 123321
        /// </remarks>
        /// <param name="functionName"></param>
        /// <returns></returns>
        [Route("{functionName}")]
        [HttpPost]
        [RequestFilter]
        public async Task<JsonResult<JObject>> Mobile(string functionName)
        {
            #region Variable
            int ResponseCode; string ResponseMessage; string Data = "null";

            ConfigJson d;
            string clientContentType; string clientAccept;
            #endregion

            #region check Request API
            // Get config
            LogHelper.WriteGeneralLog("1. Get and Check file jsonConfig");
            try
            {
                d = ApiHelper.GetApiConfig();
            }
            catch (Exception e)
            {
                ResponseCode = (int)HttpStatusCode.NotFound;//.HTTP_ERROR_SERVER;
                string msg = e.Message.ToString().Replace("'", "~").Replace(System.Environment.NewLine, "%3Cbr%3E").Replace("\\", "\\\\");
                ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"Lỗi không tồn tại file config: " + msg + "\nHoặc lỗi không đọc được định dạng Json file config: " + msg + "\", \"Data\": " + Data + "}";
                LogHelper.WriteExceptionToLog(ResponseMessage, e);
                return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));
            }
            // check HTTPS
            bool IsHttpsReq = (d.Https == 1);
            if (IsHttpsReq)
            {
                if (Request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    ResponseCode = (int)HttpStatusCode.NotAcceptable;
                    ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"2. Lỗi connect HTTPS\", \"Data\": " + Data + "}";
                    LogHelper.WriteExceptionToLog(ResponseMessage + ResponseMessage);
                    return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));
                }
            }

            LogHelper.WriteGeneralLog("3. Get and Check thông tin Accept/Content-Type");
            clientContentType = Request.Content.Headers.ContentType.ToString();
            clientAccept = Request.Headers.Accept.ToString();
            // check clientid
            LogHelper.WriteGeneralLog("4. Check Client ID");

            //get config detail by client name
            var configDetail = d.config.Find(m => m.clientId.ToUpper() == ClientType.MobileClientID.ToUpper());
            if (configDetail == null)
            {
                ResponseCode = (int)HttpStatusCode.NotFound;
                ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"4. Không tìm thấy CLIENTID\", \"Data\": " + Data + "}";
                LogHelper.WriteExceptionToLog(ResponseMessage);
                return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.NotFound);
            }

            // Check nghiệp vụ 
            var tokenInfo = RequestHelper.GetTokenInfo(Request.Headers.Authorization.ToString());

            LogHelper.WriteGeneralLog("5. Get and Check nghiệp vụ yêu cầu");
            if (functionName == null)
            {
                ResponseCode = (int)HttpStatusCode.NotFound;
                ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"5. Function name is null!\", \"Data\": " + Data + "}";
                LogHelper.WriteExceptionToLog(ResponseMessage);
                return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.NotFound);
            }
            functionName = functionName.ToLower();

            //Find function name
            var findFunc = configDetail.functionListName.Find(m => m.FunctionName.ToUpper() == functionName.ToUpper());

            if (findFunc == null)
            {
                ResponseCode = (int)HttpStatusCode.NotFound;
                ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"5. Chức năng không hợp lệ\", \"Data\": " + Data + "}";
                LogHelper.WriteExceptionToLog(ResponseMessage + ResponseMessage);
                return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.NotFound);
            }

            // get request
            string content = "";
            content = await Request.Content.ReadAsStringAsync();
            if (content == null || content.Trim() == "") content = "";

            dynamic varJsonRequest = null;
            LogHelper.WriteGeneralLog("7. Get and Check Json Request");
            if (content != "")
            {
                try
                {
                    varJsonRequest = JObject.Parse(content);
                }
                catch
                {
                    ResponseCode = (int)HttpStatusCode.BadRequest;
                    ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"7. Request no json: " + content + "\", \"Data\": " + Data + "}";
                    LogHelper.WriteExceptionToLog(ResponseMessage + ResponseMessage);
                    return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.BadRequest);
                }
            }

            string StoreName = findFunc.StoreProceduceName;
            bool IsInputList = (findFunc.IsParamInputList == 1);
            dynamic dParam = null;

            #endregion

            #region Runing API
            string parameterOutput = ""; string json = ""; int errorCode = (int)HttpStatusCode.Accepted; string errorString = "";
            string StoreParam = "";
            try
            {
                LogHelper.WriteGeneralLog("8. Function [" + functionName + "] ==> Starting execute DATA");
                var rsCheckTokenR = _authen.CheckToken(tokenInfo, functionName);
                var dJsonRsR = JObject.Parse(rsCheckTokenR.StringResult);
                if (functionName == "loginwithtoken") return Json(JObject.FromObject(dJsonRsR[functionName]));// Bỏ functionname
                //Check token result
                if (!rsCheckTokenR.BoolResult)
                {
                    var dJsonRs = JObject.Parse(rsCheckTokenR.StringResult);
                    return Json(JObject.FromObject(dJsonRs[functionName]));
                }
                //Check user permission
                var rsCheckPer = await _authen.CheckPermission(tokenInfo.UserID, functionName);
                if (!rsCheckPer.BoolResult)
                {
                    var dJsonRs = JObject.Parse(rsCheckPer.StringResult);
                    return Json(JObject.FromObject(dJsonRs[functionName]));
                }
              
                //Init store params
                StoreParam = ApiHelper.ConvertStoreParameter(functionName, findFunc, tokenInfo, varJsonRequest);
                if (StoreParam != "") dParam = JObject.Parse("{\"parameterInput\":[" + StoreParam + "]}");

                //Thực thi store procedure nghiệp vụ
                //Case 1 số trường hợp đặc biệt call service nghiệp vụ qua các class partial tương ứng
                switch (functionName.ToUpper())
                {
                    case "REGISTERLEAVE":
                        return await API_RegisterLeave(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR);
                    case "REGISTEROT":
                        return await API_RegisterOT(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR);
                    case "REGISTERWLEO":
                        return await API_RegisterWLEO(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR);

                }
                //Các case khác thực hiện thủ tục
                //using (var conn = new OracleDBManager(StoreName, System.Data.CommandType.StoredProcedure, ConnectionString))
                //{
                //    conn.ExecuteStoreAPI(functionName, StoreName, dParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
                //}
                _dBTool.ExecuteStoreAPI(functionName, StoreName, dParam, ref parameterOutput, ref json, ref errorCode, ref errorString);

                //Các trường hợp Approve cần xác định bước kế tiếp (chuyển timesheet, sendmail) -> chuyển forward sang service
                switch (functionName.ToUpper())
                {
                    case "APPROVEWLEO":
                        await API_ApproveRegister(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR, "WLEO", 2, json);
                        break;
                    case "APPROVEREGISTERLEAVE":
                        await API_ApproveRegister(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR, "LEAVE", 2, json);
                        break;
                    case "APPROVEREGISTEROT":
                        await API_ApproveRegister(functionName, dParam, tokenInfo.CompanyCode, tokenInfo.UserID, rsCheckTokenR, "OVERTIME", 2, json);
                        break;
                }


                if (errorCode != (int)HttpStatusCode.Accepted)
                {
                    LogHelper.WriteGeneralLog("8.3. Function [" + functionName + "] ==> Lỗi: " + errorString + "; json: " + json);
                    return Json(JObject.FromObject(new ResponseData() { Message = errorString, Error = HttpStatusCode.NotAcceptable.ToString() }));//StatusCode(HttpStatusCode.NotAcceptable);
                }

                dynamic dJson = JObject.Parse(json);
                long status = 1; bool IsResponseStatus = true; string Msg = "";
                try { status = long.Parse(dJson[functionName].ResponseStatus.ToString()); Msg = dJson[functionName].Message.ToString(); } catch { Msg = ""; status = 1; IsResponseStatus = false; }//(Exception ex)

                if (!IsResponseStatus)
                    return Json(dJson[functionName]);// Không có ResponseStatus
                else if (status > 0)
                    return Json(dJson[functionName]);// ResponseStatus > 0
                else                             // ResponseStatus < 1
                    return Json(JObject.Parse("{\"ResponseStatus\": " + status.ToString() + ", \"Message\": \"" + Msg + "\"}"));
            }
            catch (Exception e)
            {
                ResponseCode = (int)HttpStatusCode.NotFound; 
                string msg = e.Message.Replace("'", "~").Replace(System.Environment.NewLine, "%3Cbr%3E").Replace("\\", "\\\\");
                ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"8. Function [" + functionName + "] ==> " + msg + "\", \"Data\": " + Data + "}";
                LogHelper.WriteExceptionToLog(ResponseMessage, e);
                return Json(JObject.FromObject(new ResponseData() { Message = "Internal system error", Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.NotFound);
            }
            #endregion
        }

        [Route("MobileView/TimeSheet/MonthDetail")]
        [HttpGet]
        public HttpResponseMessage TimeSheetMonthDetail()
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/MobileView/TimeSheet/MonthDetail?"
                + HttpContext.Current.Request.QueryString.ToString() + "&renew=1");
            return response;
        }

        [Route("MobileView/PayslipMobile/payslip")]
        [HttpGet]
        public HttpResponseMessage PaySlip()
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/MobileView/PayslipMobile/payslip?"
                + HttpContext.Current.Request.QueryString.ToString() + "&renew=1");
            return response;
        }

        [HttpPost]
        [Route("Employee/EmployeeImage")]
        public async Task<IHttpActionResult> GetEmployeeImageAsync()
        {
            var response = new BaseJsonResponse<object>();
            string clientContentType; string clientAccept;
            string content = "";
            int ResponseCode; string ResponseMessage; string Data = "null";
            try
            {
                clientContentType = Request.Content.Headers.ContentType.ToString();
                clientAccept = Request.Headers.Accept.ToString();
                // Check nghiệp vụ 
                var tokenInfo = RequestHelper.GetTokenInfo(Request.Headers.Authorization.ToString());
                var rsCheckTokenR = _authen.CheckToken(tokenInfo, "EmployeeImage");
                if (!rsCheckTokenR.BoolResult)
                {
                    var dJsonRs = JObject.Parse(rsCheckTokenR.StringResult);
                    return Json(JObject.FromObject(dJsonRs["EmployeeImage"]));
                }
                //Check user permission
                var rsCheckPer = await _authen.CheckPermission(tokenInfo.UserID, "EmployeeImage");
                if (!rsCheckPer.BoolResult)
                {
                    var dJsonRs = JObject.Parse(rsCheckPer.StringResult);
                    return Json(JObject.FromObject(dJsonRs["EmployeeImage"]));
                }
                content = await Request.Content.ReadAsStringAsync();
                //dynamic varJsonRequest = null;
                var JsonRequest = new EmployeeImageRequest();
                if (content != "")
                {
                    try
                    {
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        JsonRequest = json_serializer.Deserialize<EmployeeImageRequest>(content.ToString());
                    }
                    catch
                    {
                        ResponseCode = (int)HttpStatusCode.BadRequest;
                        ResponseMessage = "{\"Error\":\"" + ResponseCode + "\", \"Message\": \"7. Request no json: " + content + "\", \"Data\": " + Data + "}";
                        LogHelper.WriteExceptionToLog(ResponseMessage + ResponseMessage);
                        return Json(JObject.FromObject(new ResponseData() { Message = ResponseMessage, Error = ResponseCode.ToString() }));//StatusCode(HttpStatusCode.BadRequest);
                    }
                }
                //String  EmployeeId=HttpContext.Current.Request.QueryString.Get("Id").ToString();
                using var profileBusinessClient = new ProfileBusinessClient();
                EmployeeImageRequest request = new EmployeeImageRequest(JsonRequest.userId , JsonRequest.sError );
                
                var data = await profileBusinessClient.EmployeeImageAsync(request);
                response.Status = true;
                response.Message = request.sError;
                response.Data = new
                {
                    Image = data.EmployeeImageResult // Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(JsonConvert.SerializeObject(data.EmployeeImageResult))) ,
                    //Message = data.sError
                };
                response.Message  = HttpStatusCode.OK.ToString();
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }
    }
}
