using HiStaffAPI.AppCommon.Config;
using HiStaffAPI.AppCommon.MobileModel;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace HiStaffAPI.AppHelpers
{
    public class ApiHelper
    {
        const string configName = "APICONFIG";
        const string configOMName = "APIOMCONFIG";
        /// <summary>
        /// Link truy cập Portal cho template gửi email
        /// </summary>
        public static readonly string PortalUrl = ConfigurationManager.AppSettings.Get("PortalUrl");
        public static readonly string PortalPort = ConfigurationManager.AppSettings.Get("PortalPort");
        public static readonly string DomainEmail = ConfigurationManager.AppSettings.Get("DomainEmail");
        public static readonly string PathFolderFaceImage = ConfigurationManager.AppSettings.Get("PathFolderFaceImage");
        public static readonly string FaceServices = ConfigurationManager.AppSettings.Get("FaceServices");
        public static void ReadApiConfig()
        {
            var strCurDir = AppDomain.CurrentDomain.BaseDirectory + "\\config.json";
            if (File.Exists(strCurDir))
            {
                var strConfig = File.ReadAllText(strCurDir);
                var jsonObj = JsonConvert.DeserializeObject<ConfigJson>(strConfig);
                MemoryCacheHelper.Clear(configName);
                MemoryCacheHelper.Add(configName, jsonObj);
            }
        }

        public static ConfigJson GetApiConfig()
        {
            var rs = MemoryCacheHelper.Get<ConfigJson>(configName);
            if (rs == null)
            {
                ReadApiConfig();
                rs = MemoryCacheHelper.Get<ConfigJson>(configName);
            }
            return rs;
        }

        public static void ReadApiOMConfig()
        {
            var strCurDir = AppDomain.CurrentDomain.BaseDirectory + "\\configOM.json";
            ReadFile(strCurDir, configOMName);
        }

        public static ConfigJson GetApiOMConfig()
        {
            var rs = MemoryCacheHelper.Get<ConfigJson>(configOMName);
            if (rs == null)
            {
                ReadApiOMConfig();
                rs = MemoryCacheHelper.Get<ConfigJson>(configOMName);
            }
            return rs;
        }

        /// <summary>
        /// Đọc file và lưu Mem cache
        /// </summary>
        /// <param name="strCurDir"></param>
        /// <param name="configName"></param>
        public static void ReadFile(string strCurDir, string configName)
        {
            if (File.Exists(strCurDir))
            {
                var strConfig = File.ReadAllText(strCurDir);
                var jsonObj = JsonConvert.DeserializeObject<ConfigJson>(strConfig);
                MemoryCacheHelper.Clear(configName);
                MemoryCacheHelper.Add(configName, jsonObj);
            }
        }

        /// <summary>
        /// Hàm gán các giá trị mặc định cho Param store và cộng chuỗi Params
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="findFunc"></param>
        /// <param name="request"></param>
        /// <param name="varJsonRequest"></param>
        /// <returns></returns>
        public static string ConvertStoreParameter(string functionName, ConfigFunction findFunc, TokenApiDTO request, dynamic varJsonRequest)
        {
            string StoreParam = "";
            if (varJsonRequest != null)
            {
                for (int j = 0; j < findFunc.ParamIn.Count; j++) // Param In
                {
                    if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "companycode")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.CompanyCode);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "deviceid")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.DeviceID);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "userid")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.UserID);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "token")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.Token);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "language")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.Language);
                    else
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), varJsonRequest[findFunc.ParamIn[j].ParamName.ToString()].ToString());
                }
                for (int j = 0; j < findFunc.ParamOut.Count; j++) // Param In
                {
                    StoreParam = StoreParam + "," + findFunc.ParamOut[j].ParamJson.ToString();
                }
            }
            else
            {
                for (int j = 0; j < findFunc.ParamIn.Count; j++) // Param In
                {
                    if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "companycode")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.CompanyCode);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "deviceid")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.DeviceID);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "userid")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.UserID);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "token")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.Token);
                    else if (functionName != "login" && findFunc.ParamIn[j].ParamName.ToString().ToLower() == "language")
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString().Replace("REQUEST" + findFunc.ParamIn[j].ParamName.ToString(), request.Language);
                    else
                        StoreParam = StoreParam + "," + findFunc.ParamIn[j].ParamJson.ToString();
                }
                for (int j = 0; j < findFunc.ParamOut.Count; j++) // Param In
                {
                    StoreParam = StoreParam + "," + findFunc.ParamOut[j].ParamJson.ToString();
                }
            }
            if (StoreParam != "") StoreParam = StoreParam.Substring(1); 
            return StoreParam;
        }

    }
}
