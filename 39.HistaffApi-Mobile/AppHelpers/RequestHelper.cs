using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppException;
using Newtonsoft.Json;
using System;
using System.Text;

namespace HiStaffAPI.AppHelpers
{
    /// <summary>
    /// Truy cập dữ liệu Request và trả về các thông tin cần thiết
    /// </summary>
    public class RequestHelper
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        /// <summary>
        /// Lấy chuỗi xác thực
        /// </summary>
        /// <returns></returns>
        public static string GetAuthorizeString()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string auth = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(auth))
            {
                return auth;
            }
            else return "";
        }

        /// <summary>
        /// Lấy thông tin Authorization và deserialize ra object TokenDTO (Portal)
        /// </summary>
        /// <returns></returns>
        public static TokenApiDTO GetTokenInfo()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            var getAuthor = context.Request.Headers["Authorization"];
            if (getAuthor == null) throw new UnauthorizeException("Require authorization data");
            return JsonConvert.DeserializeObject<TokenApiDTO>(Encoding.UTF8.GetString(Convert.FromBase64String(getAuthor.ToString().Replace("Bearer ", ""))));
        }

        /// <summary>
        /// Chuyển chuỗi token (mobile) thành object
        /// </summary>
        /// <param name="authHeader"></param>
        /// <returns></returns>
        public static AppCommon.MobileModel.TokenApiDTO GetTokenInfo(string authHeader)
        {
            string txtAuthorization = StringHelper.Base64Decode(authHeader.Replace("Bearer ", "").Trim());
            string[] arrAuthorization = txtAuthorization.Split(new string[] { "^" }, StringSplitOptions.None);

            var rs = new AppCommon.MobileModel.TokenApiDTO();
            rs.DeviceID = arrAuthorization.Length > 0 ? arrAuthorization[0] : "";//deviceID
            rs.CompanyCode = arrAuthorization.Length > 1 ? arrAuthorization[1] : "";
            rs.UserID = arrAuthorization.Length > 2 ? arrAuthorization[2] : "";
            rs.Token = arrAuthorization.Length > 3 ? arrAuthorization[3] : "";
            rs.Language = arrAuthorization.Length > 4 ? arrAuthorization[4] : "vi-VN";

            return rs;
        }
    }
}
