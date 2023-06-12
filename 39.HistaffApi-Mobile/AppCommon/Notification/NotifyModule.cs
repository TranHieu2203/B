using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.MobileModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiStaffAPI.AppCommon.Notification
{
    public static class NotifyModule
    {
        private static string ConnectionString = ConfigurationManager.AppSettings["DbConnection"];

        /// <summary>
        /// Lấy và gửi noti firebase (được gọi từ action user gửi noti)
        /// </summary>
        /// <returns></returns>
        public async static Task<bool> GetAndSendNotification(bool isPortal, string iRegisterID = "0", string sRegisterType = "")
        {
            try
            {
                var StoreParam = new CustomStoreParam();
                var strUserId = "";
                if (isPortal)
                {
                    strUserId = RequestHelper.GetTokenInfo().UserId;
                }
                else
                {
                    strUserId = RequestHelper.GetTokenInfo(HttpContext.Current.Request.Headers["Authorization"].ToString()).UserID;
                }

                StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            {
                new CustomStoreParam.ParamObject(){ ParamName = "InputRegisterID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "20", InputValue = iRegisterID },
                new CustomStoreParam.ParamObject(){ ParamName = "InputRegisterType", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "20", InputValue = sRegisterType },
                new CustomStoreParam.ParamObject(){ ParamName = "UserID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "20", InputValue = strUserId},
                new CustomStoreParam.ParamObject(){ ParamName = "Status", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "20", InputValue = "00"},
                new CustomStoreParam.ParamObject(){ ParamName = "Page", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "4", InputValue = "1"},
                new CustomStoreParam.ParamObject(){ ParamName = "PageSize", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "4", InputValue = "10"},
                new CustomStoreParam.ParamObject(){ ParamName = "DeviceIds", ParamType= "12", ParamInOut = ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Cur", ParamType= "121", ParamInOut =ParameterDirection.Output, ParamLength = "0", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Rowcount", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
                new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
            };

                //var result = _authen.ExecuteStore(requestApi, ProcedureName.API_GetWaitNotification, "GetAndSendNotification", StoreParam);
                string parameterOutput = "", json = "", errorString = "";
                int errorCode = 0;
                using (var conn = new OracleDBManager(ProcedureName.API_GetWaitNotification, System.Data.CommandType.StoredProcedure, ConnectionString))
                {
                    conn.ExecuteStoreAPI("GetAndSendNotification", ProcedureName.API_PackageDefault + "."+ ProcedureName.API_GetNotificationAll, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
                }

                string strListId = "0";

                if (errorCode >= 0 && !string.IsNullOrEmpty(json))
                {
                    var rs = JObject.Parse(json)["GetAndSendNotification"];
                    //todo: lặp và send firebase
                    foreach (var item in rs["Items"])
                    {
                        if (item["DEVICE_IDS"] != null)
                        {
                            FirebaseHelper.SendNotify(item["MESSAGE_TITLE"].ToString(), item["MESSAGE_BODY"].ToString(), "", item["DEVICE_IDS"].ToString().Split(','));
                            strListId += "," + item["ID"];
                        }
                    }
                }
                //update cứng đã gửi thành công
                await UpdateSentNotification(strListId, "02");

                return await Task.FromResult(true);// Json(JObject.Parse(json));
            }
            catch
            {
                //1 số exception do chưa khai báo store procedure (notify mobile)
                return await Task.FromResult(false);
            }
        }

        public async static Task<bool> UpdateSentNotification(string listIds, string status)
        {
            if (listIds == "0") return false;
            var StoreParam = new CustomStoreParam();

            StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            {
                new CustomStoreParam.ParamObject(){ ParamName = "StringIds", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "200", InputValue = listIds},
                new CustomStoreParam.ParamObject(){ ParamName = "Status", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "10", InputValue = status},
                new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
            };

            string parameterOutput = "", json = "", errorString = "";
            int errorCode = 0;
            using (var conn = new OracleDBManager(ProcedureName.API_UpdateSentNotification, CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI(ProcedureName.API_UpdateSentNotification, ProcedureName.API_PackageDefault + "." + ProcedureName.API_UpdateSentNotification, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
            }
            var rs = JObject.Parse(json)["API_UpdateSentNotification"];

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Lưu notification vào DB
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async static Task<bool> SaveNotification(NotificationContent request)
        {
            //ViewerUserID IN NUMBER,
            //ActorUserID IN NUMBER,
            //EntityTypeID IN NUMBER,
            //EntityID IN NUMBER,
            //MessageNotification IN NVARCHAR2,
            //Message OUT NVARCHAR2,
            //ResponseStatus OUT NUMBER
            var StoreParam = new CustomStoreParam();
            StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            {
                new CustomStoreParam.ParamObject(){ ParamName = "ViewerUserID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "19", InputValue = request.ToEmployeeID.ToString() },
                new CustomStoreParam.ParamObject(){ ParamName = "ActorUserID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "20", InputValue = request.FromEmployeeID.ToString()},
                new CustomStoreParam.ParamObject(){ ParamName = "EntityTypeID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "4", InputValue = request.SVALUE.ToString()},
                new CustomStoreParam.ParamObject(){ ParamName = "EntityID", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "10", InputValue = request.NVALUE.ToString()},
                new CustomStoreParam.ParamObject(){ ParamName = "MessageNotification", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "200", InputValue = request.MessageContent},
                new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
            };
            string parameterOutput = "", json = "", errorString = "";
            int errorCode = 0;
            using (var conn = new OracleDBManager(ProcedureName.API_SaveNotification, CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI(ProcedureName.API_SaveNotification, ProcedureName.API_PackageDefault + "." +ProcedureName.API_SaveNotification, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
            }
            var rs = JObject.Parse(json)["SaveNotification"];
            if (rs["ResponseStatus"].ToString() == "1")
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}