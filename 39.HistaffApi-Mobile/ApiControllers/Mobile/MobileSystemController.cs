using HiStaffAPI.AppCommon;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.MobileModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Attributes;
using HiStaffAPI.CommonBusiness;
using HiStaffAPI.Extension;
using HiStaffAPI.ProfileBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using RestSharp;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace HiStaffAPI.ApiControllers.Mobile
{
    [RoutePrefix(ApiName.PrefixApiSystem)]
    public class MobileSystemController : ApiController
    {
        private readonly IAuthen _authen;

        private string _forgetPassUsername;
        //private DateTime _startValidate;
        //private string _endValidate;

        //private readonly string ConnectionString = ConfigurationManager.AppSettings["DbConnection"];
        public MobileSystemController(IAuthen authen)
        {
            _authen = authen;
        }

        /// <summary>
        /// Đăng nhập với request là json object username, password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public JsonResult<JObject> Login([FromBody] LoginRequest request)
        {
            int responseCode; string responseMessage; string data = "null";
            long status = 1; bool IsResponseStatus = true; string Msg = ""; var functionName = "login";
            string json = ""; int errorCode = 0; string errorString = "";
            if (request == null)
            {
                responseCode = (int)HttpStatusCode.NotFound;
                responseMessage = "Incorect Username or password.";
                //responseMessage = "{\"Error\":\"" + responseCode + "\", \"Message\": \"8. Function [" + functionName + "] ==> Request Is Null\", \"Data\": " + data + "}";
                LogHelper.WriteExceptionToLog(responseMessage + responseMessage);
                return Json(JObject.FromObject(new ResponseData() { Message = responseMessage, Error = responseCode.ToString() }));//StatusCode(HttpStatusCode.BadRequest);
            }
            _authen.LoginWithHistaffUser(request, ref json, ref errorCode, ref errorString);
            dynamic dJson = JObject.Parse(json);
            try
            {
                status = long.Parse(dJson[functionName].ResponseStatus.ToString()); Msg = dJson[functionName].Message.ToString();
            }
            catch
            {
                Msg = ""; status = 1; IsResponseStatus = false;
            }
            if (!IsResponseStatus)
                return Json(dJson[functionName]);// Không có ResponseStatus
            else if (status > 0)
            {
                var companyCode = dJson[functionName].ParameterOutput.CompanyCode.ToString();
                var userID = dJson[functionName].ParameterOutput.UserID.ToString();
                var token = dJson[functionName].ParameterOutput.Token.ToString();
                dJson[functionName].ParameterOutput["Token"] = StringHelper.Base64Encode(request.DeviceId + "^" + companyCode + "^" + userID + "^" + token + "^" + request.Language);//DeviceID^CompanyCode^UserID^Token
                return Json(dJson[functionName]);
            }
            else
                return Json(JObject.Parse("{\"ResponseStatus\": " + status.ToString() + ", \"Message\": \"" + Msg + "\"}"));
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<JsonResult<JObject>> Logout([FromBody] LogoutRequest request)
        {
            string authHeader = Request.Headers.Authorization.ToString();
            var requestApi = RequestHelper.GetTokenInfo(authHeader);

            var StoreParam = new CustomStoreParam();
            StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            {
                new CustomStoreParam.ParamObject(){ ParamName = "Token", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "200", InputValue = requestApi.Token  },
                new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
            };
            var requestExeStore = new RequestApiDTO()
            {
                CompanyCode = requestApi.CompanyCode,
                DeviceID = requestApi.DeviceID,
                UserID = requestApi.UserID,
                Token = requestApi.Token,
                Language = requestApi.Language
            };
            var result = _authen.ExecuteStore(requestExeStore, ProcedureName.API_PackageDefault + "." + ProcedureName.API_User_Logout, "Logout", JObject.FromObject(StoreParam));
            var rs = JObject.Parse(result.StringResult)["Logout"];
            return await Task.FromResult(Json(JObject.FromObject(rs)));
        }

        [Route("ForgotPassword")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult<JObject>> ForgotPasswordSubmit([FromBody] ForgotPasswordModel request)
        {
            String _server, _port, _account, _password, _status;
            Boolean _authen, _ssl;


            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            //Lấy thông tin qua username + email
            //Xác nhận đúng thông tin thì generate 1 link có time expiry (5' - 10')
            //Xác nhận thông tin mật khẩu mới -> ok

            // Lấy code random
            //var rand = new Random();
            //for(int i =0;i < 6;i++)
            //{
            //    _changeCode = _changeCode + chars.Substring(rand.Next(1, chars.Length - 1), 1);
            //}

            //if (request.UserName.Length < 5) return Json(JObject.FromObject(new ResponseExecute() { Message = "Tên đăng nhập chưa hợp lệ", ResponseStatus = -2 }));
            //if (!Regex.IsMatch(request.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) return Json(JObject.FromObject(new ResponseExecute() { Message = "Email không hợp lệ", ResponseStatus = -3 }));
            _forgetPassUsername = request.UserName;

            using var commonBusinessClient = new CommonBusinessClient();

            //Kiểm tra và xác nhận email và user name trùng trong DB
            //GetUserWithPermision
            var userInfo = await commonBusinessClient.GetUserWithPermisionAsync(request.UserName);
            if (userInfo == null) return Json(JObject.FromObject(new ResponseExecute() { Message = "Tài khoản không tồn tại", ResponseStatus = -4 }));
            if (userInfo.EMAIL == null) return Json(JObject.FromObject(new ResponseExecute() { Message = "Tài khoản chưa được thiết lập email, bạn vui lòng liên hệ bộ phận nhân sự để reset mật khẩu", ResponseStatus = -5 }));

            // Get config mail
            var config = await commonBusinessClient.GetConfigAsync(SystemConfigModuleID.All);
            _server = config.ContainsKey("MailServer") ? config["MailServer"] : "";
            _port = config.ContainsKey("MailPort") ? config["MailPort"] : "";
            _account = config.ContainsKey("MailAccount") ? config["MailAccount"] : "";
            _password = config.ContainsKey("MailAccountPassword") ? config["MailAccountPassword"] : "";
            _ssl = config.ContainsKey("MailIsSSL") ? Convert.ToBoolean( decimal.Parse( config["MailIsSSL"])) : false;
            _authen = config.ContainsKey("MailIsAuthen") ? Convert.ToBoolean(decimal.Parse(config["MailIsAuthen"])) : false;

            if (_password.ToString() != "")
            {
                _password = new EncryptData().DecryptString(_password.ToString());
            }

            var dataMail = await commonBusinessClient.GET_MAIL_TEMPLATEAsync("QUEN_MK", "Common");

            var contentTemp = dataMail[0];

            contentTemp = string.Format(contentTemp, request.ActCode);

            //Gửi email link xác nhận

            MailHelper MailMessage = new MailHelper(_server.ToString(), int.Parse(_port.ToString()), _account.ToString(), _password);
            MailMessage.From = _account;
            MailMessage.To = userInfo.EMAIL;
            MailMessage.Subject = "Xác nhận đổi mật khẩu Histaff";
            MailMessage.BodyHTML = contentTemp;

            var data = MailMessage.SendMail();

            if (data == "SUCCESSFUL")
            {
                var rs = new ResponseExecute() { Message = "", ResponseStatus = 1 };
                rs.Message = "Hệ thống đã gửi mã xác nhận về email của bạn. Lưu ý mã xác nhận chỉ tồn tại trong 60s!";
                rs.ResponseStatus = 1;
                return Json(JObject.FromObject(rs));
            }
            else
            {
                return Json(JObject.FromObject(new ResponseExecute() { Message = "Yêu cầu không được thực hiện do lỗi dịch vụ. Xin chờ trong ít phút và thử lại!", ResponseStatus = -600 }));
            }
        }

        [Route("password-change")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult<JObject>> PasswordChange([FromBody] ForgotPasswordModel request)
        {
            using var commonBusinessClient = new CommonBusinessClient();

            if (request.PassWord == null ) return Json(JObject.FromObject(new ResponseExecute() { Message = "Chưa nhập mật khẩu", ResponseStatus = -4 }));
            if (request.PassWord.Trim() == "") return Json(JObject.FromObject(new ResponseExecute() { Message = "Chưa nhập mật khẩu", ResponseStatus = -4 }));

            var data = await commonBusinessClient.ChangePassByForgetAsync(request.UserName, request.PassWord);
            if (data)
            {
                var rs = new ResponseExecute() { Message = "", ResponseStatus = 1 };
                rs.Message = "Đổi mật khẩu thành công";
                rs.ResponseStatus = 1;
                return Json(JObject.FromObject(rs));
            }
            else
            {
                return Json(JObject.FromObject(new ResponseExecute() { Message = "Mật khẩu mới không hợp lệ", ResponseStatus = -600 }));
            }

        }




        [Route("UserProfile/UploadAvatar")]
        [HttpPatch]
        public async Task<IHttpActionResult> UploadAvatar([FromBody] UploadAvatarRequest request)
        {
            var rsData = new ResponseData();
            if (!string.IsNullOrEmpty(request.Base64String))
            {
                var token = RequestHelper.GetTokenInfo(RequestHelper.GetAuthorizeString());

                byte[] bytes = Convert.FromBase64String(request.Base64String);
                var userID = token.UserID ;// request.EmployeeId;
                var emageEx = request.ImageEx;
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.PortalSendImageAsync("", (decimal.Parse(userID)), bytes, emageEx);
                if (data)
                {
                    rsData.Data = token.UserID;
                    rsData.Message = "Success";
                    rsData.Error = "1";
                    return Json(rsData);
                }
            }
            rsData.Message = "NoAction";
            rsData.Error = (int)HttpStatusCode.NotAcceptable + "";
            return Json(rsData);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
		[Route("upload-face-detect")]
        [HttpPost]
        public async Task<IHttpActionResult>UploadFaceDetect([FromBody] UploadFaceDetectRequest request)
        {
            var rsData = new ResponseData();
            try
            {
                if (!string.IsNullOrEmpty(request.Base64String))
                {
                    var token = RequestHelper.GetTokenInfo(RequestHelper.GetAuthorizeString());
                    var userID = token.UserID  ;
                    var emageEx = request.ImageEx;
                    byte[] bytes = Convert.FromBase64String(request.Base64String);
                    //-------------------------------------check face id and validate face........
                    
                    FaceServicesRequest faceServicesRequest = new FaceServicesRequest 
                    { 
                        user_id =decimal.Parse( userID),
                        user_name ="",
                        image_base64 =request.Base64String ,
                        lable_predict=1 
                    };
                    var res= FaceServices.Api_Face_Predict(faceServicesRequest);
                    FaceServicesData json = JsonConvert.DeserializeObject<FaceServicesData>(res.Result.Content);
                    //end check-------------------------------------------------------------------
                    if (res.Result.StatusCode ==HttpStatusCode.OK)
                    {
                        int lable = (int)json.lable_predict;
                        //lable_predict = -1 ->DETECT_FACE_NOTFOUND
                        //THONG BAO
                        string subPath = "lable" + lable.ToString();
                        //phan lop de hoc tang cuong
                        string pathFolder = ApiHelper.PathFolderFaceImage;
                        string pathFolderUser = pathFolder + "/" + userID.ToString() + "/" + subPath + "/";
                        Directory.CreateDirectory(pathFolderUser);
                        String imageName = userID.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + emageEx;
                        MemoryStream memoryStream = new MemoryStream(bytes);
                        FileStream fs = new FileStream(pathFolderUser + "/" + imageName, FileMode.Create);
                        memoryStream.WriteTo(fs);
                        rsData.Data = request.UserId;
                        rsData.Message = "Success";
                        rsData.Error = "1";
                        memoryStream.Close();
                        memoryStream.Dispose();
                        fs.Close();
                        fs.Dispose();
                        return await Task.Run(() => Json(rsData));
                    }
                    //return  Json(rsData);
                }
                rsData.Message = "NoAction";
                rsData.Error = (int)HttpStatusCode.NotAcceptable + "";
                return await Task.Run(() => Json(rsData));
            }catch(Exception ex)
            {
                rsData.Message = "NoAction";
                rsData.Error = (int)HttpStatusCode.NotAcceptable + " "+ex.ToString ();
                //return Json(rsData);
                return await Task.Run(() => Json(rsData));
            }
            
        }
        /// <summary>
        /// Đổi mật khẩu với thông tin json object ChangePassRequest
        /// </summary>
        /// <param name="changePassRequest"></param>
        /// <returns></returns>
        [PortalAuthorize]
        [Route("ChangeUserPassword")]
        [HttpPost]
        public async Task<ParamOutput> ChangePassword([FromBody] ChangePassRequest changePassRequest)
        {
            try
            {
                   //Lấy thông tin token -> user logon
                //var token = RequestHelper.GetTokenInfo(Request.Headers.Authorization.ToString());
                var token = RequestHelper.GetTokenInfo(RequestHelper.GetAuthorizeString());


                using var commonBusinessClient = new CommonBusinessClient();
                var userInfo = commonBusinessClient.GetUserByUserID(token.UserID.ToDecimal().Value);
                if (userInfo == null)
                {
                    return await Task.FromResult(new ParamOutput() { Message = token.Language == "vi-VN" ? "Không tìm thấy tài khoản" : "User not found", ResponseStatus = -600 });
                }
                else
                {
                    if (changePassRequest.UserName.ToDecimal() != userInfo.ID) //username = userid
                    {
                        return await Task.FromResult(new ParamOutput() { Message = token.Language == "vi-VN" ? "Không tìm thấy tài khoản" : "UserName not found", ResponseStatus = -600 });
                    }
                }
                var userID = token.UserID;


                bool rsChanged = false;

                //thay đổi password -> forward tới WCF xử lý
                using (var repCommon = new CommonBusinessClient())
                {
                    rsChanged = await repCommon.ChangeUserPasswordAsync(userInfo.USERNAME, changePassRequest.PasswordOld, changePassRequest.Password
                        , new CommonBusiness.UserLog() { Username = userInfo.USERNAME, Ip = "", ComputerName = "" });
                }
                //return result
                if (rsChanged)
                {
                    return await Task.FromResult(new ParamOutput() { Message = token.Language == "vi-VN" ? "Đổi mật khẩu thành công" : "Password is changed", ResponseStatus = 1 });
                }
                else
                {
                    return await Task.FromResult(new ParamOutput() { Message = token.Language == "vi-VN" ? "Đổi mật khẩu thất bại, kiểm tra lại mật khẩu cũ" : "Change password is fail", ResponseStatus = -600 });
                } 
            }
            catch (Exception e)
            {
                LogHelper.WriteExceptionToLog("ERROR: " + e);
                return null;
            }

            


        }

        public class ChangePassRequest
        {
            public string UserName { set; get; }
            public string PasswordOld { set; get; }
            public string Password { set; get; }
        }

    }
    public class UploadAvatarRequest
    {
        public string Base64String { set; get; }
        public string EmployeeId { set; get; }
        public string ImageEx { get; set; }
    }
    public class UploadFaceDetectRequest
    {
        public string Base64String { set; get; }
        public string UserId { set; get; }
        public string ImageEx { get; set; }
    }
}