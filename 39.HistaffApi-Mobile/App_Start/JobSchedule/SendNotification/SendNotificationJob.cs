using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.Notification;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiStaffAPI.App_Start.JobSchedule.SendNotification
{
    public class SendNotification : IJob
    {
        private readonly IOracleDBManager _dBTool;

        //public SendBirthdayJob() { }

        public SendNotification(IOracleDBManager DBTool)
        {
            _dBTool = DBTool;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                //Lấy dữ liệu
                var StoreParam = new CustomStoreParam
                {
                    parameterInput = new List<CustomStoreParam.ParamObject>()
                    {
                        new CustomStoreParam.ParamObject(){ ParamName = "Page", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "4", InputValue = "1"},
                        new CustomStoreParam.ParamObject(){ ParamName = "PageSize", ParamType= "8", ParamInOut = ParameterDirection.Input, ParamLength = "4", InputValue = "100"},
                        new CustomStoreParam.ParamObject(){ ParamName = "Language", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "5", InputValue = "vi-VN"},
                        new CustomStoreParam.ParamObject(){ ParamName = "DeviceIds", ParamType= "12", ParamInOut = ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                        new CustomStoreParam.ParamObject(){ ParamName = "Cur", ParamType= "121", ParamInOut =ParameterDirection.Output, ParamLength = "0", InputValue = ""},
                        new CustomStoreParam.ParamObject(){ ParamName = "Rowcount", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
                        new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                        new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
                    }
                };

                //var result = _authen.ExecuteStore(requestApi, ProcedureName.API_GetWaitNotification, "GetAndSendNotification", StoreParam);
                string parameterOutput = "", json = "", errorString = "";
                int errorCode = 0;
                // _dBTool.ExecuteStoreAPI("API_GetNotificationAll", ProcedureName.API_PackageDefault + "." + ProcedureName.API_GetNotificationAll, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
               _dBTool.ExecuteStoreAPI(ProcedureName.API_GetNotificationAll, ProcedureName.API_PackageDefault + "."
                    + ProcedureName.API_GetNotificationAll, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
                string strListId = "0";

                if (errorCode >= 0 && !string.IsNullOrEmpty(json))
                {
                    var rs = JObject.Parse(json)[ProcedureName.API_GetNotificationAll];
                    //todo: lặp và send firebase
                    foreach (var item in rs["Items"])
                    {
                        if (item["DEVICE_IDS"] != null)
                        {
                            FirebaseHelper.SendNotify(item["FROM_EMPLOYEE_NAME"].ToString(), item["MESSAGE_BODY_STYLE"].ToString()
                                , "", item["DEVICE_IDS"].ToString().Split(','));
                            strListId += "," + item["ID"];
                        }
                    }

                    //update cứng đã gửi thành công
                    Task.WaitAll(NotifyModule.UpdateSentNotification(strListId, "3"));
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("API_GetNotificationAll", ex);
                return null;
            }
        }
    }
}