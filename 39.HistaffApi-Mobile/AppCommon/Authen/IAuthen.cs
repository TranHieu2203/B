using HiStaffAPI.AppCommon.MobileModel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiStaffAPI.AppCommon.Authen
{
    public interface IAuthen
    {
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
        void LoginWithHistaffUser(LoginRequest request, ref string json, ref int errorCode, ref string errorString); // API Mobile


        /// <summary>
        /// Login bằng HRM Account (Web App - iPortal)
        /// </summary>
        /// <param name="Username">Tên tài khoản</param>
        /// <param name="UserPassword">Mật khẩu</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        void LoginWithHistaffUser(string Username, string UserPassword, out dynamic d); //Web app


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
        bool LoginWithADUser(string LdapServerID, string Username, string UserPassword, string DeviceID,
            ref string json, ref int errorCode, ref string errorString);// API Mobile

        ExecuteResult CheckToken(TokenApiDTO _token, string functionName);

        /// <summary>
        /// Login bằng LDAP Account (Web App)
        /// </summary>
        /// <param name="LdapServerID">ID LDAP Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu LDAP</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        bool LoginWithADUser(string LdapServerID, string Username, string UserPassword, out dynamic d); //Web app


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
        bool LoginWithEmail(string MailServerID, string Username, string UserPassword, string DeviceID, ref string json,
            ref int errorCode, ref string errorString);// API Mobile


        /// <summary>
        /// Login bằng Mail Account (Web App - iPortal)
        /// </summary>
        /// <param name="MailServerID">ID Mail Server</param>
        /// <param name="Username">Tên tài khoản hệ thống</param>
        /// <param name="UserPassword">Mật khẩu email</param>
        /// <param name="d">Object Json gồm biến Output và table nếu có</param>
        /// <returns></returns>
        bool LoginWithEmail(string MailServerID, string Username, string UserPassword, out dynamic d); //Web app

        /// <summary>
        /// LoginWithHistaffUser_WCF
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="UserPassword"></param>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        Task<ResponseData> LoginWithHistaffUser_WCF(string Username, string UserPassword, string DeviceID);

        /// <summary>
        /// Kiểm tra quyền theo thủ tục (AppMobile)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        Task<ExecuteResult> CheckPermission(string userID, string functionName);

        ExecuteResult ExecuteStore(RequestApiDTO request, string storeName, string functionName, JObject StoreParamm);
    }
}