using Autofac;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.MobileModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.CommonBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace HiStaffAPI.AppCommon.Authen
{
    public class AuthenOM : IAuthenOM
    {
        #region Properties
        private readonly string ConnectionString = ConfigurationManager.AppSettings["DbOMConnection"];
        #endregion

        public AuthenOM()
        {
        }

        #region Private Method
        /// <summary>
        /// Lấy thông tin User hệ thống; dùng cho xác thực qua mail server hay LDAP (App Mobile)
        /// </summary>
        /// <param name="Username">Tên tài khoản</param>
        /// <param name="DeviceID">ID thiết bị</param>
        /// <param name="json">Json format API</param>
        /// <param name="errorCode">Mã HTTP_CODE</param>
        /// <param name="errorString">Thông báo lỗi</param>
        /// <returns></returns>
        private void GetHistaffUserByEmail(string Username, string DeviceID, ref string json, ref int errorCode, ref string errorString)// API Mobile
        {
            string parameterOutput = "";
            dynamic d1 = JObject.Parse("{\"parameterInput\":[" +
                "{\"ParamName\": \"UserName\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"60\", \"InputValue\": \"" + Username + "\"}," +
                "{\"ParamName\": \"DeviceID\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"100\", \"InputValue\": \"" + DeviceID + "\"}," +
                "{\"ParamName\": \"CompanyID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"CompanyCode\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"60\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"UserID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"FullName\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Email\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Mobile\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"40\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Avatar\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Token\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"UserType\", \"ParamType\": \"16\", \"ParamInOut\": \"3\", \"ParamLength\": \"2\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"ImageID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"StaffID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"Message\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"600\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"ResponseStatus\", \"ParamType\": \"8\", \"ParamInOut\": \"3\", \"ParamLength\": \"4\", \"InputValue\": \"0\"}]}");
         
            using (var conn = new OracleDBManagerOM("SP_CMS__Users_GetInfoByEmail", System.Data.CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI("login", "SP_CMS__Users_GetInfoByEmail", d1, ref parameterOutput, ref json, ref errorCode, ref errorString);
            }
        }

        /// <summary>
        /// Lấy thông tin User hệ thống; dùng cho xác thực qua mail server hay LDAP (Web APP)
        /// </summary>
        /// <param name="Username">Tên tài khoản</param>
        /// <param name="DeviceID">Để trống => PC WEB</param>
        /// <param name="d">Object Json trả về các Param Output[ParamInOut=3]</param>
        /// <returns></returns>
        private void GetHistaffUserByEmail(string Username, out dynamic d)// Web app
        {
            string parameterOutput = ""; string json = ""; int errorCode = 0; string errorString = "";
            dynamic d1 = JObject.Parse("{\"parameterInput\":[" +
                "{\"ParamName\": \"UserName\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"60\", \"InputValue\": \"" + Username + "\"}," +
                "{\"ParamName\": \"DeviceID\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"100\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"CompanyID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"CompanyCode\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"60\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"UserID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"FullName\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Email\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Mobile\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"40\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Avatar\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Token\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"UserType\", \"ParamType\": \"16\", \"ParamInOut\": \"3\", \"ParamLength\": \"2\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"ImageID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"StaffID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"Message\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"600\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"ResponseStatus\", \"ParamType\": \"8\", \"ParamInOut\": \"3\", \"ParamLength\": \"4\", \"InputValue\": \"0\"}]}");

            using (var conn = new OracleDBManagerOM("SP_CMS__Users_GetInfoByEmail", System.Data.CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI("Authen", "SP_CMS__Users_GetInfoByEmail", d1, ref parameterOutput, ref json, ref errorCode, ref errorString, false);
            }
            d = JObject.Parse("{" + parameterOutput + "}");
        }

        /// <summary>
        /// Lấy thông tin mail server theo mã công ty (Khách hàng - Bản triển khai 01 khách = 01 mã khách hàng)
        /// </summary>
        /// <param name="CompanyCode">Mã công ty hệ thống tự xác định thông qua Get Infor tài khoản</param>
        /// <param name="MailServerID">ID mail server chọn</param>
        /// <param name="domain">Domain của mail server</param>
        /// <param name="smtp">Địa chỉ mail server gửi</param>
        /// <param name="port">Port của mail server</param>
        /// <param name="IsSSL">1. Mail server SSL; 0. Mail Server thường</param>
        /// <returns></returns>
        private bool GetMailServerInfo(
            string CompanyCode, string MailServerID,
            out string domain, out string smtp, out int port, out bool IsSSL)// Mail server
        {
            try
            {

                string parameterOutput = ""; string json = ""; int errorCode = 0; string errorString = "";
                dynamic d1 = JObject.Parse("{\"parameterInput\":[" +
                    "{\"ParamName\": \"Sys_MailServerID\", \"ParamType\": \"0\", \"ParamInOut\": \"1\", \"ParamLength\": \"8\", \"InputValue\": \"" + MailServerID + "\"}," +
                    "{\"ParamName\": \"Keyword\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                    "{\"ParamName\": \"Page\", \"ParamType\": \"8\", \"ParamInOut\": \"1\", \"ParamLength\": \"4\", \"InputValue\": \"1\"}," +
                    "{\"ParamName\": \"PageSize\", \"ParamType\": \"8\", \"ParamInOut\": \"1\", \"ParamLength\": \"4\", \"InputValue\": \"100\"}," +
                    "{\"ParamName\": \"Rowcount\", \"ParamType\": \"8\", \"ParamInOut\": \"3\", \"ParamLength\": \"4\", \"InputValue\": \"0\"}]}");

                using (var conn = new OracleDBManagerOM("SP_CMS__Sys_MailServer_List", System.Data.CommandType.StoredProcedure, ConnectionString))
                {
                    conn.ExecuteStoreToDataset("MailServer", "SP_CMS__Sys_MailServer_List", d1, ref parameterOutput, ref json, ref errorCode, ref errorString, true);
                }

                d1 = JObject.Parse(json);
                domain = "";// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.MailServer, 0, "EmailDomain");
                smtp = "";// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.MailServer, 0, "SMTPServer");
                port = 0;// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.MailServer, 0, "Port");
                IsSSL = false;// (Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.MailServer, 0, "IsSSL") == 1);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("GetMailServerInfo Is Error: " + ex.ToString(), ex);
                domain = ""; smtp = ""; port = 0; IsSSL = false;
                return false;
            }

        }

        /// <summary>
        /// Lấy thông tin LDAP server theo mã công ty (Khách hàng - Bản triển khai 01 khách = 01 mã khách hàng)
        /// </summary>
        /// <param name="CompanyCode">Mã công ty hệ thống tự xác định thông qua Get Infor tài khoản</param>
        /// <param name="LdapServerID">ID LDAP server chọn</param>
        /// <param name="LdapURL">Đường dẫn LDAP</param>
        /// <param name="LDAPDomain">Domain LDAP</param>
        /// <param name="LDAPBaseDN">BaseDN LDAP</param>
        /// <returns></returns>
        private bool GetLdapServerInfo(string CompanyCode, string LdapServerID,
            out string LdapURL, out string LDAPDomain, out string LDAPBaseDN)// Ldap
        {
            try
            {
                string parameterOutput = ""; string json = ""; int errorCode = 0; string errorString = "";
                dynamic d1 = JObject.Parse("{\"parameterInput\":[" +
                    "{\"ParamName\": \"Sys_LDAPID\", \"ParamType\": \"0\", \"ParamInOut\": \"1\", \"ParamLength\": \"8\", \"InputValue\": \"" + LdapServerID + "\"}," +
                    "{\"ParamName\": \"Keyword\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                    "{\"ParamName\": \"Page\", \"ParamType\": \"8\", \"ParamInOut\": \"1\", \"ParamLength\": \"4\", \"InputValue\": \"1\"}," +
                    "{\"ParamName\": \"PageSize\", \"ParamType\": \"8\", \"ParamInOut\": \"1\", \"ParamLength\": \"4\", \"InputValue\": \"100\"}," +
                    "{\"ParamName\": \"Rowcount\", \"ParamType\": \"8\", \"ParamInOut\": \"3\", \"ParamLength\": \"4\", \"InputValue\": \"0\"}]}");

                using (var conn = new OracleDBManagerOM("SP_CMS__Sys_LDAP_List", System.Data.CommandType.StoredProcedure, ConnectionString))
                {
                    conn.ExecuteStoreToDataset("LdapServer", "SP_CMS__Sys_LDAP_List", d1, ref parameterOutput, ref json, ref errorCode, ref errorString, true);
                }
                
                d1 = JObject.Parse(json);
                LdapURL = "";// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.LdapServer, 0, "LdapURL");
                LDAPDomain = "";// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.LdapServer, 0, "LDAPDomain");
                LDAPBaseDN = "";// Tools.GetDataJson((_languageProcessor as LanguageProcessor).Initialize().JsonLanguage, d1.LdapServer, 0, "LDAPBaseDN");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("GetMailServerInfo Is Error: " + ex.ToString(), ex);
                LdapURL = ""; LDAPDomain = ""; LDAPBaseDN = "";
                return false;
            }
        }

        /// <summary>
        /// Xác thực tài khoản email qua Socket tới Mail Server
        /// </summary>
        /// <param name="m">Class Socket tới Mail Server</param>
        /// <param name="smtp">Địa chỉ mail server gửi</param>
        /// <param name="Email">Email cần xác thực</param>
        /// <param name="UserPassword">Password của email cần xác thực</param>
        /// <returns></returns>
        private bool EmailValidate(MailSocket m, string smtp, string Email, string UserPassword)
        {
            bool kt = false; string res = "";
            m.SendCommand("EHLO " + smtp); // EHLO mail.server.com
            res = m.GetFullResponse();
            m.SendCommand("AUTH LOGIN"); // AUTH LOGIN
            res = m.GetFullResponse();
            m.SendCommand(StringHelper.Base64Encode(Email)); // Base64Encode - Email
            res = m.GetFullResponse();
            m.SendCommand(StringHelper.Base64Encode(UserPassword)); // Base64Encode - Pwd
            res = m.GetFullResponse();
            if (res.IndexOf("Authentication successful") > 0) kt = true;
            m.SendCommand("QUIT"); // QUIT
            res = m.GetFullResponse();
            if (res.IndexOf("Authentication successful") > 0) kt = true;
            return kt;
        }
        #endregion

        /// <summary>
        /// Login bằng HRM Account (App Mobile)
        /// </summary>
        /// <param name="Username">Tên tài khoản</param>
        /// <param name="UserPassword">Mật khẩu</param>
        /// <param name="DeviceID">ID thiết bị</param>
        /// <param name="json">Chuỗi format Json gồm biến Output và table nếu có</param>
        /// <param name="errorCode">Mã HTTP_CODE</param>
        /// <param name="errorString">Thông báo kết quả</param>
        /// <returns></returns>
        public void LoginWithHistaffUser(LoginRequest request, ref string json, ref int errorCode, ref string errorString) // API Mobile
        {
            var strPass = request.Password;
            //Hash with SHA1
            using (var enc = new EncryptData())
            {
                strPass = enc.EncryptString(strPass);
            }

            string parameterOutput = "";
            dynamic StoreParam = JObject.Parse("{\"parameterInput\":[" +
                "{\"ParamName\": \"UserName\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"60\", \"InputValue\": \"" + request.UserName + "\"}," +
                "{\"ParamName\": \"Pwd\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"400\", \"InputValue\": \"" + strPass + "\"}," +
                "{\"ParamName\": \"DeviceID\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"100\", \"InputValue\": \"" + request.DeviceId + "\"}," +
                "{\"ParamName\": \"Firebase_Client_Id\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"200\", \"InputValue\": \"" + request.Firebase_Client_Id + "\"}," +
                "{\"ParamName\": \"Language\", \"ParamType\": \"12\", \"ParamInOut\": \"1\", \"ParamLength\": \"20\", \"InputValue\": \"" + request.Language + "\"}," +
                "{\"ParamName\": \"CompanyCode\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"60\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"UserID\", \"ParamType\": \"0\", \"ParamInOut\": \"3\", \"ParamLength\": \"8\", \"InputValue\": \"0\"}," +
                "{\"ParamName\": \"FullName\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Email\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Mobile\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"40\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Avatar\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"400\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Token\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"200\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"Message\", \"ParamType\": \"12\", \"ParamInOut\": \"3\", \"ParamLength\": \"600\", \"InputValue\": \"\"}," +
                "{\"ParamName\": \"ResponseStatus\", \"ParamType\": \"8\", \"ParamInOut\": \"3\", \"ParamLength\": \"4\", \"InputValue\": \"0\"}]}");
            //var StoreParam = new CustomStoreParam();
            //StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            //{
            //    new CustomStoreParam.ParamObject(){ ParamName = "UserName", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "60", InputValue = Username  },
            //    new CustomStoreParam.ParamObject(){ ParamName = "Pwd", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "400", InputValue = UserPassword  },
            //    new CustomStoreParam.ParamObject(){ ParamName = "DeviceID", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "100", InputValue = DeviceID  },
            //    new CustomStoreParam.ParamObject(){ ParamName = "Firebase_Client_Id", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "60", InputValue = request.Firebase_Client_Id },
            //    new CustomStoreParam.ParamObject(){ ParamName = "Language", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "20", InputValue = request.Language },
            //    new CustomStoreParam.ParamObject(){ ParamName = "CompanyCode", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "200", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "UserID", ParamType= "0", ParamInOut =ParameterDirection.InputOutput, ParamLength = "8", InputValue = "0"},
            //    new CustomStoreParam.ParamObject(){ ParamName = "FullName", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "200", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "Email", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "400", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "Mobile", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "40", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "Avatar", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "400", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "Token", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "200", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.InputOutput, ParamLength = "600", InputValue = ""},
            //    new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.InputOutput, ParamLength = "4", InputValue = "0"},
            //};
            using (var conn = new OracleDBManagerOM(ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_Login, CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI("login", ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_Login, StoreParam, ref parameterOutput, ref json, ref errorCode, ref errorString);
            }
        }

        /// <summary>
        /// Hàm kiểm tra token tại DB (App Mobile)
        /// </summary>
        /// <param name="_token"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public ExecuteResult CheckToken(TokenApiDTO _token, string functionName)
        {
            var request = new RequestApiDTO()
            {
                CompanyCode = _token.CompanyCode,
                DeviceID = _token.DeviceID,
                UserID = _token.UserID,
                Token = _token.Token,
                Language = _token.Language
            };
            //dynamic d = JObject.Parse("{\"parameterInput\":[" +
            //       "{\"ParamName\":\"CompanyCode\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"60\", \"InputValue\":\"" + _token.CompanyCode + "\"}," +
            //       "{\"ParamName\":\"UserID\", \"ParamType\":\"0\", \"ParamInOut\":\"1\", \"ParamLength\":\"8\", \"InputValue\":\"" + _token.UserID + "\"}," +
            //       "{\"ParamName\":\"DeviceID\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"100\", \"InputValue\":\"" + _token.DeviceID + "\"}," +
            //       "{\"ParamName\":\"Token\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"200\", \"InputValue\":\"" + _token.Token + "\"}," +
            //       "{\"ParamName\":\"UserName\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"200\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"FullName\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"200\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"Email\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"200\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"Mobile\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"20\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"Avatar\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"200\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"Message\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"600\", \"InputValue\":\"\"}," +
            //       "{\"ParamName\":\"ResponseStatus\", \"ParamType\":\"8\", \"ParamInOut\":\"3\", \"ParamLength\":\"4\", \"InputValue\":\"0\"}]}");
            var StoreParam = new CustomStoreParam();
            StoreParam.parameterInput = new List<CustomStoreParam.ParamObject>()
            {
                new CustomStoreParam.ParamObject(){ ParamName = "CompanyCode", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "60", InputValue = _token.CompanyCode},
                new CustomStoreParam.ParamObject(){ ParamName = "UserID", ParamType= "0", ParamInOut =ParameterDirection.Input, ParamLength = "8", InputValue = _token.UserID},
                new CustomStoreParam.ParamObject(){ ParamName = "DeviceID", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "100", InputValue =  _token.DeviceID },
                new CustomStoreParam.ParamObject(){ ParamName = "Language", ParamType= "12", ParamInOut = ParameterDirection.Input, ParamLength = "100", InputValue =  _token.Language },
                new CustomStoreParam.ParamObject(){ ParamName = "Token", ParamType= "12", ParamInOut =ParameterDirection.Input, ParamLength = "200", InputValue =  _token.Token},
                new CustomStoreParam.ParamObject(){ ParamName = "UserName", ParamType= "12", ParamInOut = ParameterDirection.Output, ParamLength = "60", InputValue = ""  },
                new CustomStoreParam.ParamObject(){ ParamName = "FullName", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "200", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Email", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "400", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Mobile", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "40", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Avatar", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "400", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "Message", ParamType= "12", ParamInOut =ParameterDirection.Output, ParamLength = "600", InputValue = ""},
                new CustomStoreParam.ParamObject(){ ParamName = "ResponseStatus", ParamType= "8", ParamInOut =ParameterDirection.Output, ParamLength = "10", InputValue = "0"},
            };

            return ExecuteStore(request, ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_LoginWToken, functionName, JObject.FromObject(StoreParam));
        }


        /// <summary>
        /// Check permission từ store procedure (App Mobile)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public async Task<ExecuteResult> CheckPermission(string userID, string functionName)
        {
            return await Task.Run(() =>
            {
                var rs = new ExecuteResult();
                rs.BoolResult = false;
                LogHelper.WriteGeneralLog("8.2. Function [CheckToken] ==> Khởi tạo connect string DB");
                string json = ""; string errorString = "", parameterOutput = "";
                int errorCode = 0;
                dynamic d = JObject.Parse("{\"parameterInput\":[" +
                    "{\"ParamName\":\"UserID\", \"ParamType\":\"0\", \"ParamInOut\":\"1\", \"ParamLength\":\"8\", \"InputValue\":\"" + userID + "\"}," +
                    "{\"ParamName\":\"Url\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"400\", \"InputValue\":\"/API/MobileOM/" + functionName + "\"}," +
                    "{\"ParamName\":\"Message\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"600\", \"InputValue\":\"\"}," +
                    "{\"ParamName\":\"ResponseStatus\", \"ParamType\":\"8\", \"ParamInOut\":\"3\", \"ParamLength\":\"4\", \"InputValue\":\"0\"}]}");
                using (var conn = new OracleDBManagerOM(ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_CheckPermission, System.Data.CommandType.StoredProcedure, ConnectionString))
                {
                    conn.ExecuteStoreAPI(functionName, ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_CheckPermission, d, ref parameterOutput, ref json, ref errorCode, ref errorString);
                }
                LogHelper.WriteGeneralLog("8.4. Function [CheckPermission] ==> Data: " + json);
                rs.StringResult = json;
                try
                {
                    dynamic d1 = JObject.Parse(json); //Msg = d.ParameterOutput.Message.ToString();
                    if ((long)d1[functionName].ResponseStatus > 0)
                    {
                        rs.BoolResult = true;
                        return rs;
                    }
                    else
                    {
                        LogHelper.WriteGeneralLog("8.4. Function [CheckPermission] ==> errorString: " + errorString + "; Message: " + d1[functionName].Message.ToString());
                        rs.BoolResult = false;
                        return rs;
                    }
                }
                catch (Exception e)
                {
                    //Msg = "Có lỗi!";
                    LogHelper.WriteGeneralLog("8.4. Function [CheckPermission] ==> Error: " + e.ToString().Replace("'", "~").Replace(System.Environment.NewLine, "%3Cbr%3E").Replace("\\", "\\\\"));
                    rs.StringResult = "{'Error':'500','Message':'Exception','Data':'" + e.Message + "'}";
                    return rs;
                }
            });
        }


        public ExecuteResult ExecuteStore(RequestApiDTO request, string storeName, string functionName, JObject StoreParamm)
        {

            var rs = new ExecuteResult();
            LogHelper.WriteGeneralLog("8.2. Function [" + functionName + "] ==> Khởi tạo connect string DB");

            string json = ""; string errorString = "", parameterOutput = "";
            int errorCode = 0;

            using (var conn = new OracleDBManagerOM(storeName, System.Data.CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI(functionName, storeName, StoreParamm, ref parameterOutput, ref json, ref errorCode, ref errorString);
            }
            LogHelper.WriteGeneralLog("8.3. Function [" + functionName + "] ==> Data: " + json);

            try
            {
                dynamic d1 = JObject.Parse(json); //Msg = d.ParameterOutput.Message.ToString();
                rs.StringResult = json;
                if ((long)d1[functionName].ResponseStatus > 0)
                {
                    rs.BoolResult = true;
                    //add thêm các thuộc tính cố định
                    d1[functionName]["ParameterOutput"].UserID = request.UserID;
                    d1[functionName]["ParameterOutput"].CompanyCode = request.CompanyCode;
                    d1[functionName]["ParameterOutput"].Token = request.Token;
                    d1[functionName]["ParameterOutput"].DevideId = request.DeviceID;
                    rs.StringResult = JsonConvert.SerializeObject(d1);
                }
                else
                {
                    LogHelper.WriteGeneralLog("8.3. Function  [" + functionName + "] ==> errorString: " + errorString + "; Message: " + d1[functionName].Message.ToString());
                    rs.StringResult = JsonConvert.SerializeObject(d1);
                }
                return rs;
            }
            catch (Exception e)
            {
                //Msg = "Có lỗi!";
                LogHelper.WriteExceptionToLog("8.3. Function  [" + functionName + "] ==> Error: " + e.ToString().Replace("'", "~").Replace(System.Environment.NewLine, "%3Cbr%3E").Replace("\\", "\\\\"));
                rs.StringResult = "{'Error':'500','Message':'Exception','Data':'" + e.Message.Replace("'", "") + "'}";
                rs.BoolResult = false;
                return rs;
            }
        }

        /// <summary>
        /// Login bằng HRM Account (Web App - iPortal)
        /// </summary>
        /// <param name="Username">Tên tài khoản</param>
        /// <param name="UserPassword">Mật khẩu</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        public void LoginWithHistaffUser(string Username, string UserPassword, out dynamic d) //Web app
        {
            string parameterOutput = ""; string json = ""; int errorCode = 0; string errorString = "";
            dynamic d1 = JObject.Parse("{\"parameterInput\":[" +
                    "{\"ParamName\":\"UserName\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"60\", \"InputValue\":\"" + Username + "\"}," +
                    "{\"ParamName\":\"Pwd\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"400\", \"InputValue\":\"" + UserPassword + "\"}," +
                    "{\"ParamName\":\"CompanyID\", \"ParamType\":\"0\", \"ParamInOut\":\"3\", \"ParamLength\":\"8\", \"InputValue\":\"0\"}," +
                    "{\"ParamName\":\"CompanyCode\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"60\", \"InputValue\":\"\"}," +
                    "{\"ParamName\":\"UserID\", \"ParamType\":\"0\", \"ParamInOut\":\"3\", \"ParamLength\":\"8\", \"InputValue\":\"0\"}," +
                    "{\"ParamName\":\"UserType\", \"ParamType\":\"16\", \"ParamInOut\":\"3\", \"ParamLength\":\"2\", \"InputValue\":\"0\"}," +
                    "{\"ParamName\":\"ImageID\", \"ParamType\":\"0\", \"ParamInOut\":\"3\", \"ParamLength\":\"8\", \"InputValue\":\"0\"}," +
                    "{\"ParamName\":\"StaffID\", \"ParamType\":\"0\", \"ParamInOut\":\"3\", \"ParamLength\":\"8\", \"InputValue\":\"0\"}," +
                    "{\"ParamName\":\"Token\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"200\", \"InputValue\":\"\"}," +
                    "{\"ParamName\":\"Message\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"600\", \"InputValue\":\"\"}," +
                    "{\"ParamName\":\"ResponseStatus\", \"ParamType\":\"8\", \"ParamInOut\":\"3\", \"ParamLength\":\"4\", \"InputValue\":\"0\"}]}");

            using (var conn = new OracleDBManagerOM(ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_Login, System.Data.CommandType.StoredProcedure, ConnectionString))
            {
                conn.ExecuteStoreAPI("Authen", ProcedureName.API_PackageDefaultOM + "." + ProcedureName.API_User_Login, d1, ref parameterOutput, ref json, ref errorCode, ref errorString, false);
            }
            d = JObject.Parse("{" + parameterOutput + "}");
        }

        /// <summary>
        /// Login bằng LDAP Account (App Mobile)
        /// </summary>
        /// <param name="LdapServerID">ID LDAP Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu LDAP</param>
        /// <param name="DeviceID">ID thiết bị</param>
        /// <param name="json">Chuỗi format Json gồm biến Output và table nếu có</param>
        /// <param name="errorCode">Mã HTTP_CODE</param>
        /// <param name="errorString">Thông báo kết quả</param>
        /// <returns></returns>
        public bool LoginWithADUser(string LdapServerID, string Username, string UserPassword, string DeviceID, ref string json, ref int errorCode, ref string errorString)// API Mobile
        {
            bool kt = false;
            LogHelper.WriteGeneralLog("LoginWithADUser Is Start");
            try
            {
                //1. Check email tồn tại => Username
                dynamic d;
                GetHistaffUserByEmail(Username, DeviceID, ref json, ref errorCode, ref errorString);// Web app
                d = JObject.Parse(json);
                if ((long)d.login.ResponseStatus < 1) return kt;
                string Email = d.login.ParameterOutput.Email.ToString(); // Tai khoan co email doc lap
                //2. Get thong tin mail server
                string CompanyCode = d.login.ParameterOutput.CompanyCode.ToString();
                string LdapURL = ""; string LDAPDomain = ""; string LDAPBaseDN = "";
                if (!GetLdapServerInfo(CompanyCode, LdapServerID, out LdapURL, out LDAPDomain, out LDAPBaseDN)) return kt;
                if (Email == "") Email = Username + "@" + LDAPDomain; // Email theo domain
                //3. Check LDap
                LDAP m = new LDAP(LdapURL, LDAPDomain, LDAPBaseDN, 0, Username, UserPassword); // 0 - ContextType.Machine
                kt = m.ValidateUser();
                LogHelper.WriteGeneralLog("LoginWithADUser Is OK");
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("LoginWithADUser Is Error: " + ex.ToString());
                json = "{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}";
                errorCode = (int)HttpStatusCode.Accepted;
                errorString = ("SystemError");
                kt = false;
            }
            return kt;
        }

        /// <summary>
        /// Login bằng LDAP Account (Web App)
        /// </summary>
        /// <param name="LdapServerID">ID LDAP Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu LDAP</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        public bool LoginWithADUser(string LdapServerID, string Username, string UserPassword, out dynamic d) //Web app
        {
            bool kt = false;
            LogHelper.WriteGeneralLog("LoginWithADUser Is Start");
            try
            {
                //1. Check email tồn tại => Username
                GetHistaffUserByEmail(Username, out d);// Web app
                if ((long)d.ParameterOutput.ResponseStatus < 1) return kt;
                string Email = d.ParameterOutput.Email.ToString(); // Tai khoan co email doc lap
                //2. Get thong tin mail server
                string CompanyCode = d.ParameterOutput.CompanyCode.ToString();
                string LdapURL = ""; string LDAPDomain = ""; string LDAPBaseDN = "";
                if (!GetLdapServerInfo(CompanyCode, LdapServerID, out LdapURL, out LDAPDomain, out LDAPBaseDN)) return kt;
                if (Email == "") Email = Username + "@" + LDAPDomain; // Email theo domain
                //3. Check LDap
                LDAP m = new LDAP(LdapURL, LDAPDomain, LDAPBaseDN, 0, Username, UserPassword); // 0 - ContextType.Machine
                kt = m.ValidateUser();
                LogHelper.WriteGeneralLog("LoginWithADUser Is OK");
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("LoginWithADUser Is Error: " + ex.ToString());
                d = JObject.Parse("{\"ParameterOutput\":{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}}");
                kt = false;
            }
            return kt;
        }

        /// <summary>
        /// Login bằng Mail Account (App Mobile)
        /// </summary>
        /// <param name="MailServerID">ID Mail Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu email</param>
        /// <param name="DeviceID">ID thiết bị</param>
        /// <param name="json">Chuỗi format Json gồm biến Output và table nếu có</param>
        /// <param name="errorCode">Mã HTTP_CODE</param>
        /// <param name="errorString">Thông báo kết quả</param>
        /// <returns></returns>
        public bool LoginWithEmail(string MailServerID, string Username, string UserPassword, string DeviceID, ref string json, ref int errorCode, ref string errorString)// API Mobile
        {
            bool kt = false;
            LogHelper.WriteGeneralLog("LoginWithEmail Is Start");
            try
            {
                //1. Check email tồn tại => Username
                dynamic d;
                GetHistaffUserByEmail(Username, DeviceID, ref json, ref errorCode, ref errorString);// Web app
                d = JObject.Parse(json);
                if ((long)d.login.ResponseStatus < 1) return kt;
                string Email = d.login.ParameterOutput.Email.ToString(); // Tai khoan co email doc lap
                //2. Get thong tin mail server
                string CompanyCode = d.login.ParameterOutput.CompanyCode.ToString();
                string domain = ""; string smtp = ""; int port = 0; bool IsSSL = false;
                if (!GetMailServerInfo(CompanyCode, MailServerID, out domain, out smtp, out port, out IsSSL)) return kt;
                if (Email == "") Email = Username + "@" + domain;
                //3. Check socket mail server
                MailSocket m = new MailSocket(smtp, port);
                kt = EmailValidate(m, smtp, Email, UserPassword);
                //4. Return
                //string parameterOutput = "\"ParameterOutput\":{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}";
                //d = JObject.Parse("{" + parameterOutput + "}");
                LogHelper.WriteGeneralLog("LoginWithEmail Is OK");
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("LoginWithEmail Is Error: " + ex.ToString());
                json = "{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}";
                errorCode = (int)HttpStatusCode.Accepted;
                errorString = ("SystemError");
                kt = false;
            }
            return kt;
        }

        /// <summary>
        /// Login bằng Mail Account (Web App - iPortal)
        /// </summary>
        /// <param name="MailServerID">ID Mail Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu email</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        public bool LoginWithEmail(string MailServerID, string Username, string UserPassword, out dynamic d) //Web app
        {
            bool kt = false;
            LogHelper.WriteGeneralLog("LoginWithEmail Is Start");
            try
            {
                //1. Check email tồn tại => Username
                GetHistaffUserByEmail(Username, out d);// Web app
                if ((long)d.ParameterOutput.ResponseStatus < 1) return kt;
                string Email = d.ParameterOutput.Email.ToString(); // Tai khoan co email doc lap
                //2. Get thong tin mail server
                string CompanyCode = d.ParameterOutput.CompanyCode.ToString();
                string domain = ""; string smtp = ""; int port = 0; bool IsSSL = false;
                if (!GetMailServerInfo(CompanyCode, MailServerID, out domain, out smtp, out port, out IsSSL)) return kt;
                if (Email == "") Email = Username + "@" + domain;
                //3. Check socket mail server
                MailSocket m = new MailSocket(smtp, port);
                kt = EmailValidate(m, smtp, Email, UserPassword);
                //4. Return
                //string parameterOutput = "\"ParameterOutput\":{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}";
                //d = JObject.Parse("{" + parameterOutput + "}");
                LogHelper.WriteGeneralLog("LoginWithEmail Is OK");
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("LoginWithEmail Is Error: " + ex.ToString());
                d = JObject.Parse("{\"ParameterOutput\":{\"ResponseStatus\":\"-99\", \"Message\":\"SystemError\"}}");
                kt = false;
            }
            return kt;
        }

        /// <summary>
        /// LoginWithHistaffUser_WCF for Portal new 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="UserPassword"></param>
        /// <param name="DeviceID"></param>
        /// <param name="json"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorString"></param>
        public async Task<ResponseData> LoginWithHistaffUser_WCF(string Username, string UserPassword, string DeviceID)
        {
            //Hash with SHA1
            using (var enc = new EncryptData())
            {
                UserPassword = enc.EncryptString(UserPassword);
            }
            UserDTO getUser = null;
            ResponseData rs = new ResponseData();
            using (var repCommon = new CommonBusinessClient())
            {
                getUser = await repCommon.GetUserWithPermisionAsync(Username);
            }

            if (getUser == null)
            {
                rs.Error = HttpStatusCode.NotFound.ToString();
                rs.Message = "UserNotFound";
                return rs;
            }

            if (getUser.ACTFLG == "I")
            {
                rs.Error = HttpStatusCode.Forbidden.ToString();
                rs.Message = "UserIsLocked";
                return rs;
            }

            if (getUser.PASSWORD == UserPassword)
            {
                if (getUser.EFFECT_DATE > DateTime.Now || (getUser.EXPIRE_DATE != null && getUser.EXPIRE_DATE <= DateTime.Now))
                {
                    rs.Error = HttpStatusCode.Forbidden.ToString();
                    rs.Message = "UsernameIsExpired";
                    return rs;
                }
                else
                {
                    if (getUser.EMPLOYEE_ID == null || getUser.EMPLOYEE_ID == 0)
                    {
                        rs.Error = HttpStatusCode.Forbidden.ToString();
                        rs.Message = "UsernameIsExpired";
                        return rs;
                    }
                    else
                    {
                        rs.Error = HttpStatusCode.Accepted.ToString();
                        rs.Message = "LoginIsSucess";
                        var token = TokenHelper.GenerateToken("MobileOM", getUser.USERNAME, getUser.EMPLOYEE_ID, getUser.EMPLOYEE_CODE, "login", "", true, DeviceID);//Generate token
                        //rs.Data = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(JsonConvert.SerializeObject(token)));
                        dynamic rsData = new System.Dynamic.ExpandoObject();
                        rsData.token = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(JsonConvert.SerializeObject(token)));
                        rsData.UserInfo = getUser;
                        rs.Data = rsData;
                        return rs;
                    }
                }
            }
            else
            {
                rs.Error = HttpStatusCode.NotAcceptable.ToString();
                rs.Message = "PasswordIsWrong";
                return rs;
            }
        }
    }

}