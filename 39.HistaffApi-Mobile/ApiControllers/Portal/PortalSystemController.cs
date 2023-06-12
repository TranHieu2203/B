using HiStaffAPI.AppCommon;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Attributes;
using HiStaffAPI.CommonBusiness;
using System.Threading.Tasks;
using System.Web.Http;

namespace HiStaffAPI.ApiControllers.Portal
{
    /// <summary>
    /// Api xử lý cho Portal: các chức năng liên quan hệ thống
    /// </summary>
    [RoutePrefix(ApiName.PrefixApiPortal)]
    [PortalAuthen]
    public class PortalSystemController : ApiController
    {
        private readonly IAuthen _authen;

        /// <summary>
        /// Constructor init DI
        /// </summary>
        /// <param name="authen"></param>
        public PortalSystemController(IAuthen authen)
        {
            _authen = authen;
        }

        /// <summary>
        /// Đăng nhập với request là json object username, password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(ApiName.Login)]
        [HttpPost]
        public async Task<ResponseData> Login([FromBody] LoginRequest loginRequest)
        {
            return await _authen.LoginWithHistaffUser_WCF(loginRequest.UserName, loginRequest.Password, "");
        }


        /// <summary>
        /// Đổi mật khẩu với thông tin json object ChangePassRequest
        /// </summary>
        /// <param name="changePassRequest"></param>
        /// <returns></returns>
        [Route(ApiName.ChangePassword)]
        [HttpPost]
        public async Task<ResponseData> ChangePassword([FromBody] ChangePassRequest changePassRequest)
        {
            //Lấy thông tin token -> user logon
            var token = RequestHelper.GetTokenInfo();

            bool rsChanged = false;

            //thay đổi password -> forward tới WCF xử lý
            using (var repCommon = new CommonBusinessClient())
            {
                rsChanged = await repCommon.ChangeUserPasswordAsync(token.Username, changePassRequest.OldPass, changePassRequest.NewPass
                    , new UserLog() { Username = token.Username, Ip = "", ComputerName = "" });
            }

            //return result
            if (rsChanged)
            {
                return await Task.FromResult(new ResponseData() { Message = "Password is changed" });
            }
            else
            {
                return await Task.FromResult(new ResponseData() { Message = "Change password is fail" });
            }

        }

    }

}